using System.Web.Mvc;
using App.Aplication;
using App.Core.Utilities;
using App.Framework.Utilities;
using App.Service.News;

namespace App.Front.Controllers
{
    public class SaleOffController : FrontBaseController
	{
		private readonly INewsService _newsService;

		public SaleOffController(INewsService newsService)
		{
			_newsService = newsService;
		}

		public ActionResult GetSaleOffByCategory(string virtualCategoryId, int page, string title)
		{
			var sortingPagingBuilder = new SortingPagingBuilder
			{
				Keywords = virtualCategoryId,
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
			var news = _newsService.PagedListByMenu(sortingPagingBuilder, paging);
			if (news.IsAny())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("GetContent", "Menu", new { page = i }));
				ViewBag.PageInfo = pageInfo;
				ViewBag.CountItem = pageInfo.TotalItems;
			}
			ViewBag.Title = title;

			return PartialView(news);
		}
	}
}