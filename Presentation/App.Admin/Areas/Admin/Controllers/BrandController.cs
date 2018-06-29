using App.Admin.Helpers;
using App.Core.Utilities;
using App.Domain.Entities.Brandes;
using App.FakeEntity.Brandes;
using App.Framework.Utilities;
using App.Service.Brandes;
using AutoMapper;
using Resources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class BrandController : BaseAdminController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [RequiredPermisson(Roles = "ViewBrand")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequiredPermisson(Roles = "ViewBrand")]
        public ActionResult Create(BrandViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", MessageUI.ErrorMessage);
                    return View(model);
                }

                var modelMap = Mapper.Map<BrandViewModel, Brand>(model);
                _brandService.Create(modelMap);

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.Brand)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    return RedirectToAction("Index");
                }

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Brand.Create: ", ex.Message));

                return View(model);
            }
        }

        [RequiredPermisson(Roles = "DeleteBrand")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var brands =
                        from id in ids
                        select _brandService.GetById(int.Parse(id));
                    _brandService.BatchDelete(brands);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Brand.Delete: ", ex.Message));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var modelMap = Mapper.Map<Brand, BrandViewModel>(_brandService.GetById(id));

            return View(modelMap);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "ViewBrand")]
        public ActionResult Edit(BrandViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", MessageUI.ErrorMessage);
                    return View(model);
                }

                var modelMap = Mapper.Map<BrandViewModel, Brand>(model);
                _brandService.Update(modelMap);

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Brand)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    return RedirectToAction("Index");
                }

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Brand.Create: ", ex.Message));

                return View(model);
            }
        }

        [RequiredPermisson(Roles = "ViewBrand")]
        public ActionResult Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;
            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords,
                Sorts = new SortBuilder
                {
                    ColumnName = "Name",
                    ColumnOrder = SortBuilder.SortOrder.Descending
                }
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };
            var brands = _brandService.PagedList(sortingPagingBuilder, paging);
            if (brands != null && brands.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }

            return View(brands);
        }
    }
}