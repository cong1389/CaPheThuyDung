using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.ContactInformation
{
    public interface IContactInfoRepository : IRepositoryBase<Domain.Entities.GlobalSetting.ContactInformation>
	{
        Domain.Entities.GlobalSetting.ContactInformation GetById(int id);

		IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> PagedList(Paging page);

		IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}