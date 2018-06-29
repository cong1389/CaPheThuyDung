using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.MailSetting
{
	public interface IMailSettingRepository : IRepositoryBase<ServerMailSetting>
	{
		ServerMailSetting GetById(int id);

		IEnumerable<ServerMailSetting> PagedList(Paging page);

		IEnumerable<ServerMailSetting> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}