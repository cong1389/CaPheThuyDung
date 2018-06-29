using App.Admin.Helpers;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.FakeEntity.Location;
using App.Framework.Utilities;
using App.Service.Locations;
using AutoMapper;
using Resources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class ProvinceController : BaseAdminController
	{
		private readonly IProvinceService _provinceService;

		public ProvinceController(IProvinceService provinceService)
		{
			_provinceService = provinceService;
		}

		[RequiredPermisson(Roles="ViewProvince")]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[RequiredPermisson(Roles="ViewProvince")]
		public ActionResult Create(ProvinceViewModel province, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(province);
				}

			    var province1 = Mapper.Map<ProvinceViewModel, Province>(province);
			    _provinceService.Create(province1);
			    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.Provinces)));
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
				ExtentionUtils.Log(string.Concat("Province.Create: ", exception.Message));
				return View(province);
			}
			return action;
		}

		[RequiredPermisson(Roles="DeleteProvince")]
		public ActionResult Delete(string[] ids)
		{
			try
			{
				if (ids.Length != 0)
				{
					var provinces = 
						from id in ids
						select _provinceService.GetById(int.Parse(id));
					_provinceService.BatchDelete(provinces);
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				ExtentionUtils.Log(string.Concat("Province.Delete: ", exception.Message));
			}
			return RedirectToAction("Index");
		}

		[RequiredPermisson(Roles="ViewProvince")]
		public ActionResult Edit(int id)
		{
			var provinceViewModel = Mapper.Map<Province, ProvinceViewModel>(_provinceService.GetById(id));
			return View(provinceViewModel);
		}

		[HttpPost]
		[RequiredPermisson(Roles="ViewProvince")]
		public ActionResult Edit(ProvinceViewModel provinceView, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(provinceView);
				}

			    var province = Mapper.Map<ProvinceViewModel, Province>(provinceView);
			    _provinceService.Update(province);
			    Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Provinces)));
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
				ExtentionUtils.Log(string.Concat("MailSetting.Create: ", exception.Message));
				return View(provinceView);
			}
			return action;
		}

		[RequiredPermisson(Roles="ViewProvince")]
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
			var provinces = _provinceService.PagedList(sortingPagingBuilder, paging);
			if (provinces != null && provinces.Any())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
				ViewBag.PageInfo = pageInfo;
			}
			return View(provinces);
		}
	}
}