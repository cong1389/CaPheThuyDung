using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Admin.Helpers;
using App.Aplication;
using App.Aplication.FileUtil;
using App.Core.Utilities;
using App.Framework.Utilities;
using App.Service.Other;
using Resources;

namespace App.Admin.Controllers
{
    public class LandingPageController : BaseAdminController
	{
		private readonly ILandingPageService _landingPageService;

		public LandingPageController(ILandingPageService landingPageService)
		{
			_landingPageService = landingPageService;
		}

		[HttpPost]
		public ActionResult Approved(string[] ids, int value)
		{
			try
			{
				if (ids.Length != 0)
				{
					var strArrays = ids;
					for (var i = 0; i < strArrays.Length; i++)
					{
						var num = int.Parse(strArrays[i]);
						var landingPage = _landingPageService.Get(x => x.Id == num);
						landingPage.Status = 3;
						_landingPageService.Update(landingPage);
					}
					Response.Cookies.Add(new HttpCookie("system_message", MessageUI.UpdateSuccess));
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				Response.Cookies.Add(new HttpCookie("system_message", "Cập nhật không thành công."));
				ExtentionUtils.Log(string.Concat("ContactInformation.Delete: ", exception.Message));
			}
			return RedirectToAction("Index");
		}

		public ActionResult Delete(string[] ids)
		{
			try
			{
				if (ids.Length != 0)
				{
					var list = 
						from id in (
							from id in ids
							select int.Parse(id)).ToList()
						select _landingPageService.Get(x => x.Id == id);
					_landingPageService.BatchDelete(list);
					Response.Cookies.Add(new HttpCookie("system_message", FormUI.DeleteSuccess));
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				Response.Cookies.Add(new HttpCookie("system_message", FormUI.DeleteFail));
				ExtentionUtils.Log(string.Concat("ContactInformation.Delete: ", exception.Message));
			}
			return RedirectToAction("Index");
		}

		public ActionResult Export()
		{
			var all = _landingPageService.GetAll();
			var landingPageExports = new List<LandingPageExport>();
			if (all.IsAny())
			{
				foreach (var landingPage in all)
				{
					var landingPageExport = new LandingPageExport
					{
						FullName = landingPage.FullName,
						DateOfBith = landingPage.DateOfBith,
						Email = landingPage.Email,
						PhoneNumber = landingPage.PhoneNumber,
						Status = Common.GetStatusLanddingPage(landingPage.Status),
						PlaceOfGift = string.Concat(landingPage.ContactInformation.Title, " - ", landingPage.ContactInformation.Address)
					};
					landingPageExports.Add(landingPageExport);
				}
			}
			ExcelUtil.ListToExcel(landingPageExports);
			return new EmptyResult();
		}

		[RequiredPermisson(Roles="ViewLandingPage")]
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
			var landingPages = _landingPageService.PagedList(sortingPagingBuilder, paging);
			if (landingPages != null && landingPages.Any())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
				ViewBag.PageInfo = pageInfo;
			}
			return View(landingPages);
		}

		[HttpPost]
		public ActionResult UnApproved(string[] ids, int value)
		{
			try
			{
				if (ids.Length != 0)
				{
					var strArrays = ids;
					for (var i = 0; i < strArrays.Length; i++)
					{
						var num = int.Parse(strArrays[i]);
						var landingPage = _landingPageService.Get(x => x.Id == num);
						landingPage.Status = 2;
						_landingPageService.Update(landingPage);
					}
					Response.Cookies.Add(new HttpCookie("system_message", MessageUI.UpdateSuccess));
				}
			}
			catch (Exception exception1)
			{
				var exception = exception1;
				Response.Cookies.Add(new HttpCookie("system_message", "Cập nhật không thành công."));
				ExtentionUtils.Log(string.Concat("ContactInformation.Delete: ", exception.Message));
			}
			return RedirectToAction("Index");
		}
	}
}