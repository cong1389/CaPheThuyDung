using App.Aplication;
using App.Aplication.Extensions;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Common;
using App.Domain.Entities.Data;
using App.FakeEntity.GenericControl;
using App.Framework.Utilities;
using App.Front.Extensions;
using App.Front.Models;
using App.Front.Models.Posts;
using App.Service.Common;
using App.Service.ContactInformation;
using App.Service.Gallery;
using App.Service.GenericControl;
using App.Service.Language;
using App.Service.Menu;
using App.Service.Post;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Front.Controllers
{
    public class PostController : FrontBaseController
    {
        private readonly IPostService _postService;

        private readonly IMenuLinkService _menuLinkService;

        private readonly IGalleryService _galleryService;
        private readonly IContactInfoService _contactInfoService;

        public PostController(
            IPostService postService
            , IMenuLinkService menuLinkService
            , IGalleryService galleryService
             , IWorkContext workContext
            , IGenericControlService genericControlService
            , ICacheManager cacheManager, IContactInfoService contactInfoService)
        {
            _postService = postService;
            _menuLinkService = menuLinkService;
            _galleryService = galleryService;
            _contactInfoService = contactInfoService;
        }

        [PartialCache("Short","*")]
        public ActionResult PostCategories(string virtualCategoryId, int page, string title, string attrs,
            string prices, string proattrs, string keywords
            , int? productOld, int? productNew)
        {
            var expression = PredicateBuilder.True<Post>();
            expression = expression.And(x => x.Status == (int)Status.Enable);
            var sortBuilder = new SortBuilder
            {
                ColumnName = "CreatedDate",
                ColumnOrder = SortBuilder.SortOrder.Descending
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };
            //if (page == 1)
            //{
            //    var viewBag = ViewBag;
            //    var postService = _postService;
            //    Expression<Func<Post, bool>> productNews = x => x.ProductNew && x.Status == (int)Status.Enable;
            //    viewBag.HotCard = postService.GetTop(3, productNews, x => x.CreatedDate).ToList();
            //}
            if (!string.IsNullOrEmpty(attrs))
            {
                var strArrays = attrs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var list = (
                    from s in strArrays
                    select int.Parse(s)).ToList();
                expression = expression.And(x => x.AttributeValues.Count(a => list.Contains(a.Id)) > 0);
                ViewBag.Attributes = list;
            }
            if (!string.IsNullOrEmpty(prices))
            {
                var priceQuery = (
                    from s in prices.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    select decimal.Parse(s)).ToList();
                var fromPrice = priceQuery[0];
                var toPrice = priceQuery[1];
                expression = expression.And(x => x.Price >= fromPrice && x.Price <= toPrice);

                ViewBag.Prices = priceQuery;
            }
            if (!string.IsNullOrEmpty(proattrs))
            {
                var attributes = proattrs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var attributeQuery = (
                    from s in attributes
                    select int.Parse(s)).ToList();
                expression = expression.And(x => attributeQuery.Contains(x.Id));

                ViewBag.ProAttributes = attributeQuery;
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                expression = expression.And(x => x.Title.Contains(keywords));
            }
            expression = expression.And(x => x.VirtualCategoryId.Contains(virtualCategoryId));

            if (productNew.HasValue)
            {
                expression = expression.And(x => !x.OldOrNew);
                ViewBag.ProductNew = productNew;
            }
            if (productOld.HasValue & productNew.HasValue)
            {
                expression = expression.Or(x => x.OldOrNew);
                ViewBag.ProductOld = productOld;
            }
            if (productOld.HasValue & !productNew.HasValue)
            {
                expression = expression.And(x => x.OldOrNew);
                ViewBag.ProductOld = productOld;
            }

            var posts = _postService.GetBySort(expression, sortBuilder, paging);
            IEnumerable<Post> postLocalized = null;

            if (posts.IsAny())
            {
                postLocalized = posts.Select(x => x.ToModel());

                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord,
                    i => Url.Action("GetContent", "Menu", new { page = i }));

                ViewBag.PageInfo = pageInfo;
                ViewBag.CountItem = pageInfo.TotalItems;
                ViewBag.MenuId = postLocalized.ElementAt(0).MenuId;
            }

            //Get menu category filter
            var menuCategoryFilter = _menuLinkService.GetByOptions(virtualId: virtualCategoryId);

            if (menuCategoryFilter.IsAny())
            {
                var lstBreadCrumb = new List<BreadCrumb>();
                ViewBag.MenuCategoryFilter = menuCategoryFilter;

                //Lấy bannerId từ post để hiển thị banner trên post
                ViewBag.BannerId = menuCategoryFilter.FirstOrDefault(x => x.VirtualId == virtualCategoryId).Id;

                var categories = virtualCategoryId.Split('/');
                for (var i = 0; i < categories.Length; i++)
                {
                    //var str = categories[i];
                    var menuLink = _menuLinkService.GetByMenuName(categories[i], title);

                    if (menuLink != null)
                    {
                        //Lấy bannerId từ post để hiển thị banner trên post
                        if (i == 0)
                        {
                            ViewBag.BannerId = menuLink.Id;
                        }

                        lstBreadCrumb.Add(new BreadCrumb
                        {
                            Title = menuLink.GetLocalized(m => m.MenuName, menuLink.Id),
                            Current = false,
                            Url = Url.Action("GetContent", "Menu", new { area = "", menu = menuLink.SeoUrl })
                        });
                    }
                }
                lstBreadCrumb.Add(new BreadCrumb
                {
                    Current = true,
                    Title = title
                });
                ViewBag.BreadCrumb = lstBreadCrumb;
            }

            ViewBag.Title = title;
            ViewBag.VirtualId = virtualCategoryId;

            return PartialView(postLocalized);
        }

        [PartialCache("Medium","*")]
        public ActionResult PostDetail(string seoUrl)
        {
            var post = _postService.GetBySeoUrl(seoUrl);

            if (post == null)
            {
                return HttpNotFound();
            }

            Post postLocalized;
            {
                postLocalized = post.ToModel();

                var viewCount = post;
                viewCount.ViewCount = viewCount.ViewCount + 1;
                _postService.Update(post);

                var categories = post.VirtualCategoryId.Split('/');
                var breadCrumbs = new List<BreadCrumb>();
                breadCrumbs.AddRange(categories.Select(str => _menuLinkService.GetByCurrentVirtualId(str))
                    .Select(menuLink => new BreadCrumb
                    {
                        Title = menuLink.GetLocalized(x => x.MenuName, menuLink.Id),
                        Current = false,
                        Url = Url.Action("GetContent", "Menu", new { area = "", menu = menuLink.SeoUrl })
                    }));
                breadCrumbs.Add(new BreadCrumb
                {
                    Current = true,
                    Title = postLocalized.Title
                });

                ViewBag.BreadCrumb = breadCrumbs;
                ViewBag.Title = postLocalized.Title;
                ViewBag.KeyWords = postLocalized.MetaKeywords;
                ViewBag.SiteUrl = Url.Action("PostDetail", "Post", new { seoUrl, area = "" });
                ViewBag.Description = postLocalized.MetaTitle;
                ViewBag.Image = Url.Content(string.Concat("~/", postLocalized.ImageBigSize));
                ViewBag.MenuId = postLocalized.MenuId;
            }

            return View(postLocalized);
        }

        [PartialCache("Medium")]
        public ActionResult PostForYou()
        {
            var posts = new List<Post>();
            var top = _postService.GetTop(20, x => x.Status == (int)Status.Enable && x.PostType == 1, x => x.CreatedDate);
            if (top.IsAny())
            {
                posts.AddRange(top);
            }
            return Json(new { data = this.RenderRazorViewToString("_PartialPostItems", posts), success = true },
                JsonRequestBehavior.AllowGet);
        }

        [PartialCache("Medium")]
        public ActionResult PostLatest()
        {
            var posts = new List<Post>();
            var tops = _postService.GetTop(20, x => x.Status == (int)Status.Enable, x => x.CreatedDate);
            if (tops.IsAny())
            {
                posts.AddRange(tops);
            }

            return Json(new { data = this.RenderRazorViewToString("_PartialPostItems", posts), success = true },
                JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        [PartialCache("Short","*")]
        public ActionResult PostSameMenu(int menuId, int postId)
        {
            var posts = new List<Post>();
            var tops = _postService.GetTop(6, x => x.Status == (int)Status.Enable && x.MenuId == menuId && x.Id != postId, x => x.CreatedDate);
            if (tops.IsAny())
            {
                posts.AddRange(tops);
            }

            return PartialView(posts);
        }

        [PartialCache("Medium")]
        public ActionResult PostPrice(int productId, int attributeId)
        {
            var galleryImage = _galleryService.Get(x => x.PostId == productId && x.AttributeValueId == attributeId);

            return galleryImage?.Price == null ? Json("Liên hệ") : Json(galleryImage.Price);
        }

        [ChildActionOnly]
        [PartialCache("Long")]
        public ActionResult PostHot()
        {
            var tops = _postService.GetTop(5, x => x.Status == (int)Status.Enable && (x.ProductHot || x.ProductNew));

            return PartialView(tops);
        }

        [PartialCache("Medium")]
        public ActionResult PostTimeLine()
        {
            var tops = _postService.GetTop(9999, x => x.Status == (int)Status.Enable && x.OldOrNew);

            return PartialView(tops);
        }

        [PartialCache("Long","*")]
        public ActionResult PostHomeNew(int page, string id)
        {
            var expression = PredicateBuilder.True<Post>();
            expression = expression.And(x => x.Status == (int)Status.Enable);
            expression = expression.And(x => x.VirtualCategoryId.Contains(id));
            expression = expression.And(x => x.ProductHot);
            var sortBuilder = new SortBuilder
            {
                ColumnName = "UpdatedDate",
                ColumnOrder = SortBuilder.SortOrder.Descending
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = 4,
                TotalRecord = 0
            };
            var posts = _postService.FindAndSort(expression, sortBuilder, paging);
            if (!posts.IsAny())
            {
                return Json(new { success = true, data = string.Empty }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                data = this.RenderRazorViewToString("_SlideProductHome",
                from x in posts
                orderby x.OrderDisplay descending
                select x),
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        [PartialCache("Long")]
        public ActionResult PostHome()
        {
            var menuLinks = _menuLinkService.GetByOptions(new List<int> { (int)Position.Top }, isDisplayHomePage: true);

            if (!menuLinks.IsAny())
            {
                return HttpNotFound();
            }

            //Convert to localized
            menuLinks = menuLinks.Select(x => x.ToModel());

            var menuParent = menuLinks.Where(x => x.ParentId == null).OrderByDescending(x => x.OrderDisplay);

            var lstPost = new List<Post>();
            foreach (var item in menuParent)
            {
                var iePost = _postService.GetByOption(item.CurrentVirtualId, true);

                if (iePost.IsAny())
                {
                    iePost = iePost.Select(x => x.ToModel());

                    lstPost.AddRange(iePost);
                }
            }

            var categoryPost = new CategoryPostModel
            {
                NumberMenu = menuParent.Count(),
                MenuLinks = menuParent,
                Posts = from x in lstPost orderby x.OrderDisplay descending select x
            };

            return PartialView(categoryPost);
        }

        [PartialCache("Medium","*")]
        public ActionResult PostHomeSearch(bool productHot, bool productNew, bool productOld)
        {
            var posts = _postService.GetTop(9999, x => x.Status == (int)Status.Enable && x.ProductHot);

            return PartialView(posts);

        }
        
        public ActionResult GetGallery(int postId, int typeId)
        {
            var galleryImages = _galleryService.GetByOption(typeId, postId);

            if (!galleryImages.IsAny())
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = this.RenderRazorViewToString("_PartialGallery", galleryImages), success = true },
                JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        [PartialCache("Short","*")]
        public ActionResult PostRelativePrice(decimal? price, int productId)
        {
            decimal nullable1 = decimal.Zero;
            decimal nullable2 = decimal.Zero;
            var nullable3 = price;
            decimal num = 2000000;
            if (nullable3.HasValue)
            {
                nullable1 = nullable3.GetValueOrDefault() - num;
            }
            else
            {
                nullable1 = decimal.Zero;
            }
            var nullable4 = nullable1;
            nullable3 = price;
            num = 2000000;
            if (nullable3.HasValue)
            {
                nullable2 = nullable3.GetValueOrDefault() + num;
            }
            else
            {
                nullable2 = decimal.Zero;
            }
            var nullable5 = nullable2;
            var posts = new List<Post>();
            var tops = _postService.GetTop(4, x => x.Status == (int)Status.Enable && x.Price >= nullable4 && x.Price <= nullable5 && x.Id != productId, x => x.UpdatedDate);

            if (tops == null)
            {
                return HttpNotFound();
            }

            if (tops.IsAny())
            {
                posts.AddRange(tops);
            }

            return PartialView(posts);
        }

        [ChildActionOnly]
        [PartialCache("Short","*")]
        public ActionResult PostRelative(string virtualId, int productId)
        {
            var posts = _postService.GetTop(10,
                x => x.Status == (int)Status.Enable && x.VirtualCategoryId.Contains(virtualId) && x.Id != productId, x => x.UpdatedDate);

            if (posts == null)
            {
                return HttpNotFound();
            }

            var postLocalized = posts.Select(x => x.ToModel());

            return PartialView(postLocalized);
        }

        [PartialCache("Medium")]
        public ActionResult SearchResult(string catUrl, string parameters, int page)
        {
            var httpCookie = HttpContext.Request.Cookies.Get("system_search");
            if (!Request.Cookies.ExistsCokiee("system_search"))
            {
                return View();
            }

            var expression = PredicateBuilder.True<Post>();
            var sortBuilder = new SortBuilder
            {
                ColumnName = "CreatedDate",
                ColumnOrder = SortBuilder.SortOrder.Descending
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };

            var seachCondition = JsonConvert.DeserializeObject<SeachConditions>(Server.UrlDecode(httpCookie.Value));

            if (!string.IsNullOrEmpty(seachCondition.Keywords))
            {
                expression = expression.And(x =>
                    x.SeoUrl.Contains(seachCondition.Keywords) || x.Title.Contains(seachCondition.Keywords) ||
                    x.Description.Contains(seachCondition.Keywords));
            }

            var posts = _postService.FindAndSort(expression, sortBuilder, paging);
            if (posts.IsAny())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord,
                    i => Url.Action("GetContent", "Menu", new { page = i }));
                ViewBag.PageInfo = pageInfo;
                ViewBag.CountItem = pageInfo.TotalItems;
            }

            ViewBag.PageNumber = page;
            ViewBag.Title = "Tìm kiếm";

            return View(posts);
        }

        #region Post discount

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult PostDiscountSlide()
        {
            return PartialView(PreparePostDiscount());
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult PostDiscountBlock()
        {
            return PartialView("_PostDetail.Discount", PreparePostDiscount().Take(5));
        }

        private IEnumerable<Post> PreparePostDiscount()
        {
            return _postService.GetByOption(isDiscount: true);
        }

        #endregion

        #region Attribute

        [HttpPost]
        public JsonResult GetByMenuId(int menuId, int entityId)
        {
            var valueItemResponses = new List<ControlValueItemResponse>();

            var menuLink = _menuLinkService.GetMenu(menuId);
            if (menuLink != null)
            {
                var genericControls = menuLink.GenericControls;
                if (genericControls.IsAny())
                {
                    valueItemResponses.AddRange(from item in genericControls
                                                from gcValue in item.GenericControlValues.Where(m => m.Status == (int)Status.Enable)
                                                select new ControlValueItemResponse
                                                {
                                                    GenericControlValueId = gcValue.Id,
                                                    Name = gcValue.ValueName,
                                                    ValueName = gcValue.GetValueItem(entityId)
                                                });
                }
            }

            var jsonResult = Json(
                new
                {
                    success = valueItemResponses.Any(),
                    list = this.RenderRazorViewToString("_PostDetail.Attribute", valueItemResponses)
                },
                JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        #endregion
    }
}