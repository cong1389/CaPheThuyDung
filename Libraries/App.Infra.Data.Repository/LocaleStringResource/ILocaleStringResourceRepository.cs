using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.LocaleStringResource
{
    public interface ILocaleStringResourceRepository : IRepositoryBase<Domain.Entities.Language.LocaleStringResource>
	{
		Domain.Entities.Language.LocaleStringResource GetLocaleStringResourceById(int id);

		IEnumerable<Domain.Entities.Language.LocaleStringResource> PagedList(Paging page);

		IEnumerable<Domain.Entities.Language.LocaleStringResource> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}