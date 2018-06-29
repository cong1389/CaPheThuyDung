using App.Admin.Helpers;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.FakeEntity.Ads;
using App.Framework.Utilities;
using App.Service.Ads;
using AutoMapper;
using Resources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class PageBannerController : BaseAdminController
    {
        private const string CachePagebannerKey = "db.PageBanner";

	    private readonly IPageBannerService _pageBannerService;

		public PageBannerController(IPageBannerService pageBannerService
            , ICacheManager cacheManager)
		{
			_pageBannerService = pageBannerService;

			//Clear cache
            cacheManager.RemoveByPattern(CachePagebannerKey);

        }

        [RequiredPermisson(Roles="CreateEditPageBanner")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[RequiredPermisson(Roles="CreateEditPageBanner")]
		public ActionResult Create(PageBannerViewModel pageBannerModel, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(pageBannerModel);
				}

			    var pageBanner = Mapper.Map<PageBannerViewModel, PageBanner>(pageBannerModel);
			    _pageBannerService.Create(pageBanner);
			    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.PageBanner)));
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
				ExtentionUtils.Log(string.Concat("PageBanner.Create: ", exception.Message));
				ModelState.AddModelError("", exception.Message);
				return View(pageBannerModel);
			}
			return action;
		}

		[RequiredPermisson(Roles="DeletePageBanner")]
		public ActionResult Delete(string[] ids)
		{
			try
			{
				if (ids.Length != 0)
				{
					var pageBanners = 
						from id in ids
						select _pageBannerService.GetById(int.Parse(id));
					_pageBannerService.BatchDelete(pageBanners);
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				ExtentionUtils.Log(string.Concat("PageBanner.Delete: ", exception.Message));
			}
			return RedirectToAction("Index");
		}

		[RequiredPermisson(Roles="CreateEditPageBanner")]
		public ActionResult Edit(int id)
		{
			var pageBannerViewModel = Mapper.Map<PageBanner, PageBannerViewModel>(_pageBannerService.GetById(id));
			return View(pageBannerViewModel);
		}

		[HttpPost]
		[RequiredPermisson(Roles="CreateEditPageBanner")]
		public ActionResult Edit(PageBannerViewModel pageBannerModel, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(pageBannerModel);
				}

			    var byId = _pageBannerService.GetById(pageBannerModel.Id);
			    var pageBanner = Mapper.Map(pageBannerModel, byId);
			    _pageBannerService.Update(pageBanner);
			    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.PageBanner)));
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
				ExtentionUtils.Log(string.Concat("PageBanner.Edit: ", exception.Message));
				return View(pageBannerModel);
			}
			return action;
		}

		[RequiredPermisson(Roles="ViewPageBanner")]
		public ActionResult Index(int page = 1, string keywords = "")
		{
			ViewBag.Keywords = keywords;
			var sortingPagingBuilder = new SortingPagingBuilder
			{
				Keywords = keywords,
				Sorts = new SortBuilder
				{
					ColumnName = "Position",
					ColumnOrder = SortBuilder.SortOrder.Descending
				}
			};
			var paging = new Paging
			{
				PageNumber = page,
				PageSize = PageSize,
				TotalRecord = 0
			};
			var pageBanners = _pageBannerService.PagedList(sortingPagingBuilder, paging);
			if (pageBanners != null && pageBanners.Any())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
				ViewBag.PageInfo = pageInfo;
			}
			return View(pageBanners);
		}
	}
}