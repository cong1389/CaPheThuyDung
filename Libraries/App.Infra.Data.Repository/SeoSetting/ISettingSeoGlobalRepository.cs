using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.SeoSetting
{
	public interface ISettingSeoGlobalRepository : IRepositoryBase<SettingSeoGlobal>
	{
		SettingSeoGlobal GetById(int id);

		IEnumerable<SettingSeoGlobal> PagedList(Paging page);

		IEnumerable<SettingSeoGlobal> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}