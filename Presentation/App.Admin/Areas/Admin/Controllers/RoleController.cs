using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Admin.Helpers;
using App.Core.Utilities;
using App.Framework.Utilities;
using App.Service.Account;

namespace App.Admin.Controllers
{
    public class RoleController : BaseAdminController
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[RequiredPermisson(Roles="ViewRole")]
		public async Task<ActionResult> Index(int page = 1, string keywords = "")
		{
			ViewBag.Keywords = keywords;
			var sortingPagingBuilder = new SortingPagingBuilder
			{
				Keywords = keywords
			};
			var sortBuilder = new SortBuilder
			{
				ColumnName = "Name",
				ColumnOrder = SortBuilder.SortOrder.Descending
			};
			sortingPagingBuilder.Sorts = sortBuilder;
			var sortingPagingBuilder1 = sortingPagingBuilder;
			var paging = new Paging
			{
				PageNumber = page,
				PageSize = PageSize,
				TotalRecord = 0
			};
			var paging1 = paging;
			var roles = await _roleService.PagedList(sortingPagingBuilder1, paging1);
			if (roles != null && roles.Any())
			{
				var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging1.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
				ViewBag.PageInfo = pageInfo;
			}
			return View(roles);
		}
	}
}