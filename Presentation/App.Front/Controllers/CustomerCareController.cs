using App.Aplication;
using App.Core.Utilities;
using App.Framework.Utilities;
using App.Front.Models;
using App.Service.Menu;
using App.Service.News;
using System.Web.Mvc;

namespace App.Front.Controllers
{
    public class CustomerCareController : FrontBaseController
	{
		private readonly INewsService _newsService;

	    public CustomerCareController(INewsService newsService, IMenuLinkService menuLinkService)
		{
			_newsService = newsService;
		}

		[ChildActionOnly]
		[PartialCache("Short")]
		public ActionResult GetCustomerCareCategory(string virtualCategoryId, int page, string title)
		{
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
			var news = _newsService.FindAndSort(x => !x.Video && x.Status == 1 && x.VirtualCategoryId.Contains(virtualCategoryId), sortBuilder, paging);
			if (news.IsAny())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("GetContent", "Menu", new { page = i }));
				ViewBag.PageInfo = pageInfo;
				ViewBag.CountItem = pageInfo.TotalItems;
			}

			ViewBag.Title = title;
			ViewBag.virtualCategoryId = virtualCategoryId;

			return PartialView(news);
		}
	}
}