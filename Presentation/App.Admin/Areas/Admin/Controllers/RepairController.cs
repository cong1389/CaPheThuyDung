using App.Admin.Helpers;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.FakeEntity.Repairs;
using App.Framework.Utilities;
using App.Service.Attribute;
using App.Service.Brandes;
using App.Service.Media;
using App.Service.Menu;
using App.Service.Repair;
using App.Service.Repairs;
using AutoMapper;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class RepairController : BaseAdminUploadController
    {
        private readonly IBrandService _brandService;

        private readonly IRepairGalleryService _galleryService;

        private readonly IRepairService _repairService;

        private readonly IRepairItemService _repairItemService;
        private readonly IImageService _imageService;

        public RepairController(IRepairService repairService, IMenuLinkService menuLinkService,
            IAttributeValueService attributeValueService, IRepairGalleryService galleryService, IBrandService brandService, IRepairItemService repairItemService
            , ICacheManager cacheManager
            , IImageService imageService)
            : base(cacheManager)
        {
            _repairService = repairService;
            _galleryService = galleryService;
            _brandService = brandService;
            _repairItemService = repairItemService;
            _imageService = imageService;
        }

        [RequiredPermisson(Roles = "CreateEditRepair")]
        public ActionResult Create()
        {
            var id = 1;
            var repair = _repairService.GetTop(1, x => x.RepairCode != null, x => x.Id).FirstOrDefault();
            if (repair != null)
            {
                id = repair.Id;
            }
            return View(new RepairViewModel
            {
                RepairCode = string.Concat("DH", id.ToString()),
                CustomerCode = string.Concat("KH", id.ToString())
            });
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditRepair")]
        [ValidateInput(false)]
        public ActionResult Create([Bind] RepairViewModel repair, string returnUrl)
        {
            ActionResult action;
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", MessageUI.ErrorMessage);
                    return View(repair);
                }

                var files = Request.Files;
                var repairGalleries = new List<RepairGallery>();
                if (files.Count > 0)
                {
                    var count = files.Count - 1;
                    var num = 0;
                    var allKeys = files.AllKeys;
                    for (var i = 0; i < allKeys.Length; i++)
                    {
                        var str = allKeys[i];
                        if (num <= count)
                        {
                            if (!str.Equals("ImageBigSize"))
                            {
                                var httpPostedFileBase = files[num];
                                if (httpPostedFileBase.ContentLength > 0)
                                {
                                    var repairGalleryViewModel = new RepairGalleryViewModel
                                    {
                                        RepairId = repair.Id
                                    };
                                    var str1 = $"{repair.RepairCode}-{Guid.NewGuid()}.jpg";
                                    _imageService.CropAndResizeImage(httpPostedFileBase,
                                        $"{Contains.MenuFolder}{repair.RepairCode}/", str1, ImageSize.WidthDefaultSize, ImageSize.HeightDefaultSize);
                                    repairGalleryViewModel.ImagePath =
                                        $"{Contains.MenuFolder}{repair.RepairCode}/{str1}";
                                    repairGalleries.Add(Mapper.Map<RepairGallery>(repairGalleryViewModel));
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
                var repair1 = Mapper.Map<RepairViewModel, Repair>(repair);
                if (repairGalleries.IsAny())
                {
                    repair1.RepairGalleries = repairGalleries;
                }
                var repairItems = new List<RepairItem>();
                if (repair.RepairItems.IsAny())
                {
                    repairItems.AddRange(
                        from item in repair.RepairItems
                        select Mapper.Map<RepairItem>(item));
                }
                if (repairItems.IsAny())
                {
                    repair1.RepairItems = repairItems;
                }
                _repairService.Create(repair1);
                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.Repair)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("Repair.Create: ", exception.Message));
                ModelState.AddModelError("", exception.Message);
                return View(repair);
            }
            return action;
        }

        [RequiredPermisson(Roles = "DeleteRepair")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var repairs = new List<Repair>();
                    var repairGalleries = new List<RepairGallery>();
                    var strArrays = ids;
                    for (var i = 0; i < strArrays.Length; i++)
                    {
                        var num = int.Parse(strArrays[i]);
                        var repair = _repairService.Get(x => x.Id == num);
                        repairGalleries.AddRange(repair.RepairGalleries.ToList());
                        repairs.Add(repair);
                    }
                    _galleryService.BatchDelete(repairGalleries);
                    _repairService.BatchDelete(repairs);
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("Repair.Delete: ", exception.Message));
            }
            return RedirectToAction("Index");
        }

        [RequiredPermisson(Roles = "CreateEditRepair")]
        public ActionResult DeleteGallery(int repairId, int galleryId)
        {
            ActionResult actionResult;
            if (!Request.IsAjaxRequest())
            {
                return Json(new { success = false });
            }
            try
            {
                var repairGallery = _galleryService.Get(x => x.RepairId == repairId && x.Id == galleryId);
                _galleryService.Delete(repairGallery);
                var str = Server.MapPath(string.Concat("~/", repairGallery.ImagePath));
                var str1 = Server.MapPath(string.Concat("~/", repairGallery.ImagePath));
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

        [RequiredPermisson(Roles = "CreateEditRepair")]
        public ActionResult Edit(int id)
        {
            var repairViewModel = Mapper.Map<Repair, RepairViewModel>(_repairService.Get(x => x.Id == id));
            return View(repairViewModel);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditRepair")]
        [ValidateInput(false)]
        public ActionResult Edit([Bind] RepairViewModel repairView, string returnUrl)
        {
            ActionResult action;
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", MessageUI.ErrorMessage);
                    return View(repairView);
                }

                var repair = _repairService.Get(x => x.Id == repairView.Id);
                var files = Request.Files;
                var lstRepairGalleries = new List<RepairGallery>();
                if (files.Count > 0)
                {
                    var count = files.Count - 1;
                    var num = 0;
                    var allKeys = files.AllKeys;
                    for (var i = 0; i < allKeys.Length; i++)
                    {
                        var str = allKeys[i];
                        if (num <= count)
                        {
                            if (!str.Equals("ImageBigSize"))
                            {
                                var item = files[num];
                                if (item.ContentLength > 0)
                                {
                                    var repairGalleryViewModel = new RepairGalleryViewModel
                                    {
                                        RepairId = repairView.Id
                                    };
                                    var str1 = $"{repairView.RepairCode}-{Guid.NewGuid()}.jpg";
                                    _imageService.CropAndResizeImage(item,
                                        $"{Contains.MenuFolder}{repairView.RepairCode}/", str1, ImageSize.WidthDefaultSize, ImageSize.HeightDefaultSize);
                                    repairGalleryViewModel.ImagePath =
                                        $"{Contains.MenuFolder}{repairView.RepairCode}/{str1}";
                                    lstRepairGalleries.Add(Mapper.Map<RepairGallery>(repairGalleryViewModel));
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
                if (lstRepairGalleries.IsAny())
                {
                    repair.RepairGalleries = lstRepairGalleries;
                }
                var repairItems = new List<RepairItem>();
                if (repairView.RepairItems.IsAny())
                {
                    foreach (var repairItem in repairView.RepairItems)
                    {
                        var repairItemViewModel = new RepairItemViewModel();
                        if (repairItem.Id > 0)
                        {
                            repairItemViewModel.Id = repairItem.Id;
                        }
                        repairItemViewModel.RepairId = repairView.Id;
                        repairItemViewModel.FixedFee = repairItem.FixedFee;
                        repairItemViewModel.Category = repairItem.Category;
                        repairItemViewModel.WarrantyFrom = repairItem.WarrantyFrom;
                        repairItemViewModel.WarrantyTo = repairItem.WarrantyTo;
                        repairItems.Add(Mapper.Map<RepairItem>(repairItemViewModel));
                    }
                }
                if (repairItems.IsAny())
                {
                    repair.RepairItems = repairItems;
                }
                var repairItems1 = _repairItemService.FindBy(x => x.RepairId == repairView.Id);
                _repairItemService.BatchDelete(repairItems1);
                repair = Mapper.Map(repairView, repair);
                _repairService.Update(repair);
                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Repair)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ModelState.AddModelError("", exception.Message);
                ExtentionUtils.Log(string.Concat("Repair.Edit: ", exception.Message));
                return View(repairView);
            }
            return action;
        }

        [RequiredPermisson(Roles = "ViewRepair")]
        public ActionResult Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;
            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords,
                Sorts = new SortBuilder
                {
                    ColumnName = "CreatedDate",
                    ColumnOrder = SortBuilder.SortOrder.Descending
                }
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };
            var repairs = _repairService.PagedList(sortingPagingBuilder, paging);
            if (repairs != null && repairs.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }
            return View(repairs);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values["action"].Equals("edit") || filterContext.RouteData.Values["action"].Equals("create"))
            {
                var viewBag = ViewBag;
                var brandService = _brandService;
                viewBag.Brands = brandService.FindBy(x => x.Status == 1);
            }
        }

        public ActionResult WarrantyForm()
        {
            var repairViewModel = new RepairViewModel();
            repairViewModel.RepairItems.Add(new RepairItemViewModel());
            return View(repairViewModel);
        }
    }
}