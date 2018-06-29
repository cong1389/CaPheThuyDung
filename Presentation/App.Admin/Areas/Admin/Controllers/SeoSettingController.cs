using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.FakeEntity.SeoGlobal;
using App.Framework.Utilities;
using App.Service.SeoSetting;
using AutoMapper;
using Resources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class SeoSettingController : BaseAdminController
    {
        private const string CacheSettingseoglobalKey = "db.SettingSeoGlobal.{0}";
	    private readonly ISettingSeoGlobalService _settingSeoGlobal;

		public SeoSettingController(ISettingSeoGlobalService settingSeoGlobal, ICacheManager cacheManager)
		{
			_settingSeoGlobal = settingSeoGlobal;

			//Clear cache
            cacheManager.RemoveByPattern(CacheSettingseoglobalKey);
        }

        public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create(SettingSeoGlobalViewModel seoSetting, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(seoSetting);
				}

			    if (seoSetting.Status == 1)
			    {
			        var settingSeoGlobals = _settingSeoGlobal.FindBy(x => x.Status == 1);
			        if (settingSeoGlobals.IsAny())
			        {
			            foreach (var settingSeoGlobal in settingSeoGlobals)
			            {
			                settingSeoGlobal.Status = 0;
			                _settingSeoGlobal.Update(settingSeoGlobal);
			            }
			        }
			    }
			    var settingSeoGlobal1 = Mapper.Map<SettingSeoGlobalViewModel, SettingSeoGlobal>(seoSetting);
			    _settingSeoGlobal.Create(settingSeoGlobal1);

			    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.SettingSeoGlobal)));
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
				ExtentionUtils.Log(string.Concat("SeoGlobal.Create: ", exception.Message));
				ModelState.AddModelError("", exception.Message);
				return View(seoSetting);
			}
			return action;
		}

		public ActionResult Delete(string[] ids)
		{
			try
			{
				if (ids.Length != 0)
				{
					var settingSeoGlobals = 
						from id in ids
						select _settingSeoGlobal.GetById(int.Parse(id));
					_settingSeoGlobal.BatchDelete(settingSeoGlobals);
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				ExtentionUtils.Log(string.Concat("SeoGlobal.Delete: ", exception.Message));
			}
			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			var settingSeoGlobalViewModel = Mapper.Map<SettingSeoGlobal, SettingSeoGlobalViewModel>(_settingSeoGlobal.GetById(id));
			return View(settingSeoGlobalViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Edit(SettingSeoGlobalViewModel seoSetting, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(seoSetting);
				}

			    var byId = _settingSeoGlobal.GetById(seoSetting.Id);
			    if (seoSetting.Status == 1 && seoSetting.Status != byId.Status)
			    {
			        var settingSeoGlobals = _settingSeoGlobal.FindBy(x => x.Status == 1);
			        if (settingSeoGlobals.IsAny())
			        {
			            foreach (var settingSeoGlobal in settingSeoGlobals)
			            {
			                settingSeoGlobal.Status = 0;
			                _settingSeoGlobal.Update(settingSeoGlobal);
			            }
			        }
			    }
			    var settingSeoGlobal1 = Mapper.Map(seoSetting, byId);
			    _settingSeoGlobal.Update(settingSeoGlobal1);
			    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.SettingSeoGlobal)));
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
				ExtentionUtils.Log(string.Concat("SeoGlobal.Edit: ", exception.Message));
				return View(seoSetting);
			}
			return action;
		}

		public ActionResult Index(int page = 1, string keywords = "")
		{
			ViewBag.Keywords = keywords;
			var sortingPagingBuilder = new SortingPagingBuilder
			{
				Keywords = keywords,
				Sorts = new SortBuilder
				{
					ColumnName = "Id",
					ColumnOrder = SortBuilder.SortOrder.Descending
				}
			};
			var paging = new Paging
			{
				PageNumber = page,
				PageSize = PageSize,
				TotalRecord = 0
			};
			var settingSeoGlobals = _settingSeoGlobal.PagedList(sortingPagingBuilder, paging);
			if (settingSeoGlobals != null && settingSeoGlobals.Any())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
				ViewBag.PageInfo = pageInfo;
			}
			return View(settingSeoGlobals);
		}
	}
}