using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Interfaces.Services;

namespace App.Service.MailSetting
{
	public interface IMailSettingService : IBaseService<ServerMailSetting>
	{
		ServerMailSetting GetById(int id);

		ServerMailSetting GetActive();

		IEnumerable<ServerMailSetting> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}