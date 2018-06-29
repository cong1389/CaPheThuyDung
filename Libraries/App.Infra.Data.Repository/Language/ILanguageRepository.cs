using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Language
{
	public interface ILanguageRepository : IRepositoryBase<Domain.Entities.Language.Language>
	{
		Domain.Entities.Language.Language GetById(int id);

		IEnumerable<Domain.Entities.Language.Language> PagedList(Paging page);

		IEnumerable<Domain.Entities.Language.Language> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}