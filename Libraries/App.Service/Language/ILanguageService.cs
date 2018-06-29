using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.Language
{
	public interface ILanguageService : IBaseService<Domain.Entities.Language.Language>
	{
		void CreateLanguage(Domain.Entities.Language.Language language);

		Domain.Entities.Language.Language GetById(int id, bool isCache = true);

		IEnumerable<Domain.Entities.Language.Language> PagedList(SortingPagingBuilder sortBuider, Paging page);

		int SaveLanguage();
	}
}