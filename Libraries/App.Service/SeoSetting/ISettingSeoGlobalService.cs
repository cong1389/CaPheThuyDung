using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Interfaces.Services;

namespace App.Service.SeoSetting
{
	public interface ISettingSeoGlobalService : IBaseService<SettingSeoGlobal>
	{
		SettingSeoGlobal GetById(int id);

		IEnumerable<SettingSeoGlobal> PagedList(SortingPagingBuilder sortBuider, Paging page);

	    SettingSeoGlobal GetEnableOrDisable(bool enable = true, bool isCache = true);

	}
}