using App.Aplication;
using App.Aplication.Extensions;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Domain.Entities.Identity;
using App.FakeEntity.Gallery;
using App.FakeEntity.Orders;
using App.FakeEntity.Post;
using App.FakeEntity.User;
using App.Framework.Utilities;
using App.Front.Extensions;
using App.Front.Models;
using App.Service.Common;
using App.Service.Gallery;
using App.Service.Locations;
using App.Service.Media;
using App.Service.Menu;
using App.Service.Orders;
using App.Service.Post;
using AutoMapper;
using Domain.Entities.Customers;
using Microsoft.AspNet.Identity;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Front.Controllers
{
    [FrontAuthorize]
	public class AccountController : BaseAccessUserController
	{
		private readonly IPostService _postService;

		private readonly IDistrictService _districtService;

		private readonly IMenuLinkService _menuLinkService;

		private readonly IGalleryService _galleryService;

		private readonly IImageService _imageService;

		private readonly IOrderService _orderService;
		private readonly IWorkContext _workContext;

		public AccountController(UserManager<IdentityUser, Guid> userManager
			, IPostService postService, IGalleryService galleryService, IProvinceService provinceService
			, IMenuLinkService menuLinkService
			, IDistrictService districtService, IImageService imageService
			, IOrderService orderService
			, IIdentityMessageService emailService
			, IWorkContext workContext) : base(userManager, emailService)
		{
			_postService = postService;
			_galleryService = galleryService;
			_menuLinkService = menuLinkService;
			_districtService = districtService;
			_imageService = imageService;
			_orderService = orderService;
			_workContext = workContext;
		}

		[HttpGet]
		public ActionResult ChangeInfo()
		{
			var registerFormViewModel = Mapper.Map<RegisterFormViewModel>(UserManager.FindByName(HttpContext.User.Identity.Name));

			return View(registerFormViewModel);
		}

		[HttpPost]
		public ActionResult ChangeInfo(RegisterFormViewModel model)
		{
			return View(model);
		}

		[HttpGet]
		public ActionResult CreatePost()
		{
			UserManager.FindByName(HttpContext.User.Identity.Name);

			return View(new PostViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(true)]
		public ActionResult CreatePost(PostViewModel post)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
				}
				else
				{
					var str = post.Title.NonAccent();
					var bySeoUrl = _postService.GetListSeoUrl(str);
					post.SeoUrl = post.Title.NonAccent();
					if (bySeoUrl.Any(x => x.Id != post.Id))
					{
						var postViewModel = post;
						postViewModel.SeoUrl = string.Concat(postViewModel.SeoUrl, "-", bySeoUrl.Count());
					}
					if (post.Image != null && post.Image.ContentLength > 0)
					{
						var str1 = $"{str}-{Utils.GetTime()}";
						var str2 = $"{str}-{Utils.GetTime()}";
						var str3 = $"{str}-{Utils.GetTime()}";
						_imageService.CropAndResizeImage(post.Image, $"{Contains.PostFolder}/{str}/", str1, ImageSize.WithBigSize, ImageSize.HeightBigSize);
						_imageService.CropAndResizeImage(post.Image, $"{Contains.PostFolder}/{str}/", str2, ImageSize.WithMediumSize, ImageSize.HeightMediumSize);
						_imageService.CropAndResizeImage(post.Image, $"{Contains.PostFolder}/{str}/", str3, ImageSize.WithSmallSize, ImageSize.HeightSmallSize);
						post.ImageBigSize = $"{Contains.PostFolder}/{str}/{str1}";
						post.ImageMediumSize = $"{Contains.PostFolder}/{str}/{str2}";
						post.ImageSmallSize = $"{Contains.PostFolder}/{str}/{str3}";
					}
					var menuId = post.MenuId;
					var i = 0;
					if (menuId.GetValueOrDefault() > i && menuId.HasValue)
					{
						var menuLinkService = _menuLinkService;
						menuId = post.MenuId;
						var byId = menuLinkService.GetMenu(menuId.Value);
						post.VirtualCatUrl = byId.VirtualSeoUrl;
						post.VirtualCategoryId = byId.VirtualId;
					}
					var files = Request.Files;
					List<GalleryImage> galleryImages = null;
					if (files.Count > 0)
					{
						galleryImages = new List<GalleryImage>();
						var count = files.Count - 1;
						var num = 0;
						var allKeys = files.AllKeys;
						for (i = 0; i < allKeys.Length; i++)
						{
							var str4 = allKeys[i];
							if (num <= count)
							{
								if (!str4.Equals("ImageBigSize"))
								{
									var item = files[num];
									if (item.ContentLength > 0)
									{
										var galleryImageViewModel = new GalleryImageViewModel
										{
											PostId = post.Id
										};
										var str5 = $"{str}-{Utils.GetTime()}";
										var str6 = $"{str}-{Utils.GetTime()}";
										_imageService.CropAndResizeImage(item, $"{Contains.PostFolder}/{str}/", str5, ImageSize.WithOrignalSize, ImageSize.HeighthOrignalSize);
										_imageService.CropAndResizeImage(item, $"{Contains.PostFolder}/{str}/", str6, ImageSize.WithThumbnailSize, ImageSize.HeightThumbnailSize);
										galleryImageViewModel.ImageThumbnail = $"{Contains.PostFolder}/{str}/{str6}";
										galleryImageViewModel.ImageBig = $"{Contains.PostFolder}/{str}/{str5}";
										galleryImageViewModel.OrderDisplay = num;
										galleryImageViewModel.Status = 1;
										galleryImageViewModel.Title = post.Title;
										galleryImages.Add(Mapper.Map<GalleryImage>(galleryImageViewModel));
									}
									num++;
								}
								else
								{
									num++;
								}
							}
						}
					}
					var post1 = Mapper.Map<PostViewModel, Post>(post);
					post1.GalleryImages = galleryImages;
					_postService.Create(post1);
					return RedirectToAction("PostManagement");
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				ExtentionUtils.Log(string.Concat("Post.Create: ", exception.Message));
				ModelState.AddModelError("", exception.Message);
			}
			return View(post);
		}

		public ActionResult DeleteGallery(int postId, int galleryId)
		{
			ActionResult actionResult;
			if (Request.IsAjaxRequest())
			{
				try
				{
					if (_postService.Get(x => x.Id == postId && x.CreatedBy.Equals(HttpContext.User.Identity.Name)) == null)
					{
						return Json(new { success = false });
					}

					var galleryImage = _galleryService.Get(x => x.PostId == postId && x.Id == galleryId);
					_galleryService.Delete(galleryImage);
					var str = Server.MapPath(string.Concat("~/", galleryImage.ImageBig));
					var str1 = Server.MapPath(string.Concat("~/", galleryImage.ImageThumbnail));
					System.IO.File.Delete(str);
					System.IO.File.Delete(str1);
					actionResult = Json(new { success = true });
				}
				catch (Exception exception1)
				{
					var exception = exception1;
					actionResult = Json(new { success = false, messages = exception.Message });
				}
				return actionResult;
			}
			return Json(new { success = false });
		}

		[HttpGet]
		public ActionResult EditPost(int id)
		{
			var postViewModel = Mapper.Map<Post, PostViewModel>(_postService.Get(x => x.Id == id && x.CreatedBy.Equals(HttpContext.User.Identity.Name)));
			return View(postViewModel);
		}

		public ActionResult EditPost(PostViewModel postView)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
				}
				else
				{
					var byId = _postService.GetById(postView.Id);
					var str = postView.Title.NonAccent();
					var bySeoUrl = _menuLinkService.GetListSeoUrl(str);
					postView.SeoUrl = postView.Title.NonAccent();
					if (bySeoUrl.Any(x => x.Id != postView.Id))
					{
						var postViewModel = postView;
						postViewModel.SeoUrl = string.Concat(postViewModel.SeoUrl, "-", bySeoUrl.Count());
					}
					var files = Request.Files;
					if (postView.Image != null && postView.Image.ContentLength > 0)
					{
						var str1 = $"{str}-{Utils.GetTime()}";
						var str2 = $"{str}-{Utils.GetTime()}";
						var str3 = $"{str}-{Utils.GetTime()}";
						_imageService.CropAndResizeImage(postView.Image, $"{Contains.PostFolder}/{str}/", str1, ImageSize.WithBigSize, ImageSize.HeightBigSize);
						_imageService.CropAndResizeImage(postView.Image, $"{Contains.PostFolder}/{str}/", str2, ImageSize.WithMediumSize, ImageSize.HeightMediumSize);
						_imageService.CropAndResizeImage(postView.Image, $"{Contains.PostFolder}/{str}/", str3, ImageSize.WithSmallSize, ImageSize.HeightSmallSize);
						postView.ImageBigSize = $"{Contains.PostFolder}/{str}/{str1}";
						postView.ImageMediumSize = $"{Contains.PostFolder}/{str}/{str2}";
						postView.ImageSmallSize = $"{Contains.PostFolder}/{str}/{str3}";
					}
					var menuId = postView.MenuId;
					var i = 0;
					if (menuId.GetValueOrDefault() > i && menuId.HasValue)
					{
						var menuLinkService = _menuLinkService;
						menuId = postView.MenuId;
						var menuLink = menuLinkService.GetMenu(menuId.Value);
						postView.VirtualCatUrl = menuLink.VirtualSeoUrl;
						postView.VirtualCategoryId = menuLink.VirtualId;
					}
					var galleryImages = new List<GalleryImage>();
					if (files.Count > 0)
					{
						var count = files.Count - 1;
						var num = 0;
						var allKeys = files.AllKeys;
						for (i = 0; i < allKeys.Length; i++)
						{
							var str4 = allKeys[i];
							if (num <= count)
							{
								if (!str4.Equals("ImageBigSize"))
								{
									var item = files[num];
									if (item.ContentLength > 0)
									{
										var galleryImageViewModel = new GalleryImageViewModel
										{
											PostId = postView.Id
										};
										var str5 = $"{str}-{Utils.GetTime()}";
										var str6 = $"{str}-{Utils.GetTime()}";
										_imageService.CropAndResizeImage(item, $"{Contains.PostFolder}/{str}/", str5, ImageSize.WithOrignalSize, ImageSize.HeighthOrignalSize);
										_imageService.CropAndResizeImage(item, $"{Contains.PostFolder}/{str}/", str6, ImageSize.WithThumbnailSize, ImageSize.HeightThumbnailSize);
										galleryImageViewModel.ImageThumbnail = $"{Contains.PostFolder}/{str}/{str6}";
										galleryImageViewModel.ImageBig = $"{Contains.PostFolder}/{str}/{str5}";
										galleryImageViewModel.OrderDisplay = num;
										galleryImageViewModel.Status = 1;
										galleryImageViewModel.Title = postView.Title;
										galleryImages.Add(Mapper.Map<GalleryImage>(galleryImageViewModel));
									}
									num++;
								}
								else
								{
									num++;
								}
							}
						}
					}
					if (galleryImages.IsAny())
					{
						byId.GalleryImages = galleryImages;
					}
					byId = Mapper.Map(postView, byId);
					_postService.Update(byId);
					ViewBag.Message = "Cập nhật tin rao thành công";
					return View(postView);
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				ModelState.AddModelError("", exception.Message);
				ExtentionUtils.Log(string.Concat("Post.Edit: ", exception.Message));
			}
			ViewBag.Message = "Cập nhật tin rao KHÔNG thành công";
			return View(postView);
		}

		public JsonResult GetDistrictByProvinceId(int provinceId)
		{
			var byProvinceId =
				from x in _districtService.GetByProvinceId(provinceId)
				select new { x.Id, x.Name };
			return Json(byProvinceId);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			AuthenticationManager.SignOut();

			return RedirectToAction("Index", "Home");
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.RouteData.Values["action"].Equals("CreatePost"))
			{
				var menuLinks = _menuLinkService.FindBy(x => x.Status == 1 && x.TemplateType != 5 && x.TemplateType != 1, true);
				ViewBag.MenuList = menuLinks;
			}
			if (filterContext.RouteData.Values["action"].Equals("EditPost"))
			{
				var menuLinks1 = _menuLinkService.FindBy(x => x.Status == 1 && x.TemplateType != 5 && x.TemplateType != 1, true);
				ViewBag.MenuList = menuLinks1;
			}
		}

		[HttpGet]
		public ActionResult PostManagement(int page = 1)
		{
			var sortBuilder = new SortBuilder
			{
				ColumnName = "CreatedDate",
				ColumnOrder = SortBuilder.SortOrder.Descending
			};
			var paging = new Paging
			{
				PageNumber = page,
				PageSize = 10,
				TotalRecord = 0
			};
			var expression = PredicateBuilder.True<Post>();
			expression = expression.And(x => x.CreatedBy.Equals(HttpContext.User.Identity.Name));
			var posts = _postService.FindAndSort(expression, sortBuilder, paging);
			if (posts.IsAny())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("PostManagement", "Account", new { page = i }));
				ViewBag.PageInfo = pageInfo;
				ViewBag.CountItem = pageInfo.TotalItems;
			}
			return View(posts);
		}

		[HttpPost]
		public ActionResult SearchPost(string keywords, int? status = null, DateTime? from = null, DateTime? to = null)
		{
			ViewBag.Keywords = keywords;
			var sortBuilder = new SortBuilder
			{
				ColumnName = "CreatedDate",
				ColumnOrder = SortBuilder.SortOrder.Descending
			};
			var paging = new Paging
			{
				PageNumber = 1,
				PageSize = 10,
				TotalRecord = 0
			};
			var expression = PredicateBuilder.True<Post>();
			expression = expression.And(x => x.CreatedBy.Equals(HttpContext.User.Identity.Name));
			if (status.HasValue)
			{
				expression = expression.And(x => (int?)x.Status == status);
			}
			if (from.HasValue)
			{
				expression = expression.And(x => x.StartDate >= from);
			}
			if (to.HasValue)
			{
				expression = expression.And(x => x.EndDate <= to);
			}
			if (!string.IsNullOrEmpty(keywords))
			{
				expression = expression.And(x => x.Title.Contains(keywords));
			}
			var posts = _postService.FindAndSort(expression, sortBuilder, paging);
			if (posts.IsAny())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, 1, paging.TotalRecord, i => Url.Action("PostManagement", "Account", new { page = i }));
				ViewBag.PageInfo = pageInfo;
				ViewBag.CountItem = pageInfo.TotalItems;
			}
			return View("PostManagement", posts);
		}

		#region Information account of customer

		public JsonResult AccountOrder()
		{
			var model = PrepareCustomerOrderListModel(_workContext.CurrentCustomer);

			var jsonResult = Json(
				new
				{
					success = true,
					list = this.RenderRazorViewToString("_Account.Order", model)
				}, JsonRequestBehavior.AllowGet);

			return jsonResult;
		}

		[NonAction]
		protected IEnumerable<OrderViewModel> PrepareCustomerOrderListModel(Customer customer)
		{
			if (customer == null)
			{
				throw new ArgumentNullException("customer");
			}

			var ieOrder = _orderService.GetByCustomerId(customer.Id, false);

			if (!ieOrder.IsAny())
			{
				return null;
			}

			ieOrder = ieOrder.OrderByDescending(m => m.Id);

			var orderViewModel = new OrderViewModel();

			var model = ieOrder.Select(x => x.ToModel(orderViewModel));

			return model;

		}

		#endregion
	}
}