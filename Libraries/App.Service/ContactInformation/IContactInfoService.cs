using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.ContactInformation
{
	public interface IContactInfoService : IBaseService<Domain.Entities.GlobalSetting.ContactInformation>
	{
        Domain.Entities.GlobalSetting.ContactInformation GetById(int id, bool isCache = true);

		IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> PagedList(SortingPagingBuilder sortBuider, Paging page);

	    Domain.Entities.GlobalSetting.ContactInformation GetTypeAddress(int typeAddress, bool isCache = true);

	    IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> GetEnableOrDisables(bool enable = true,
	        bool isCache = true);


	}
}