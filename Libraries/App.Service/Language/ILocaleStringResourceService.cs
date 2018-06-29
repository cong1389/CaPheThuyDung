using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.LocaleStringResource
{
    public interface ILocaleStringResourceService : IBaseService<Domain.Entities.Language.LocaleStringResource>
    {
        void CreateLocaleStringResource(Domain.Entities.Language.LocaleStringResource localeStringResource);

        Domain.Entities.Language.LocaleStringResource GetByName(int languageId, string resourceName, bool isCache = true);

        IEnumerable<Domain.Entities.Language.LocaleStringResource> PagedList(SortingPagingBuilder sortBuider, Paging page);

        int SaveLocaleStringResource();

        Domain.Entities.Language.LocaleStringResource GetById(int id, bool isCache = true);

        IEnumerable<Domain.Entities.Language.LocaleStringResource> GetByLanguageId(int languageId, bool isCache = true);

        IQueryable<Domain.Entities.Language.LocaleStringResource> GetAll(int languageId);

        string GetResource(string resourceKey, int languageId = 0, bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false, bool isCache = true);

    }
}