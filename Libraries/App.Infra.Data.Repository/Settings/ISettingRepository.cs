using App.Core.Utilities;
using App.Domain.Entities.Setting;
using App.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace App.Infra.Data.Repository.Settings
{
	public interface ISettingRepository : IRepositoryBase<Setting>
	{
		IEnumerable<Setting> PagedList(Paging page);

		IEnumerable<Setting> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}