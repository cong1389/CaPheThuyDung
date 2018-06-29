using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.System
{
	public interface ISystemSettingRepository : IRepositoryBase<SystemSetting>
	{
		SystemSetting GetById(int id);

		IEnumerable<SystemSetting> PagedList(Paging page);

		IEnumerable<SystemSetting> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}