using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Core.Caching;
using App.Core.Extensions;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.LocaleStringResource;
using App.Infra.Data.UOW.Interfaces;
using App.Service.Common;

namespace App.Service.LocaleStringResource
{
    public class LocaleStringResourceService : BaseService<Domain.Entities.Language.LocaleStringResource>, ILocaleStringResourceService
    {
        private const string CacheLocalestringresourceKey = "db.LocaleStringResource.{0}";

        private readonly IWorkContext _workContext;

        private readonly ILocaleStringResourceRepository _localeStringResourceRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ICacheManager _cacheManager;

        public LocaleStringResourceService(IUnitOfWork unitOfWork, ILocaleStringResourceRepository localeStringResourceRepository
            , IWorkContext workContext
            , ICacheManager cacheManager) : base(unitOfWork, localeStringResourceRepository)
        {
            _unitOfWork = unitOfWork;
            _localeStringResourceRepository = localeStringResourceRepository;
            _workContext = workContext;
            _cacheManager = cacheManager;
        }

        public void CreateLocaleStringResource(Domain.Entities.Language.LocaleStringResource localeStringResource)
        {
            _localeStringResourceRepository.Add(localeStringResource);
        }

        public Domain.Entities.Language.LocaleStringResource GetByName(int languageId, string resourceName, bool isCache = true)
        {
            Domain.Entities.Language.LocaleStringResource locale;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalestringresourceKey, "GetByName");
                sbKey.AppendFormat("-{0}", languageId);

                if (resourceName.HasValue())
                {
                    sbKey.AppendFormat("-{0}", resourceName);
                }

                var key = sbKey.ToString();
                locale = _cacheManager.Get<Domain.Entities.Language.LocaleStringResource>(key);
                if (locale == null)
                {
                    locale = _localeStringResourceRepository.Get(x
                    => x.LanguageId == languageId && x.ResourceName == resourceName);
                    _cacheManager.Put(key, locale);
                }
            }
            else
            {
                locale = _localeStringResourceRepository.Get(x
                   => x.LanguageId == languageId && x.ResourceName == resourceName);
            }

            return locale;
        }

        public IEnumerable<Domain.Entities.Language.LocaleStringResource> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _localeStringResourceRepository.PagedSearchList(sortbuBuilder, page);
        }

        public int SaveLocaleStringResource()
        {
            return _unitOfWork.Commit();
        }

        public Domain.Entities.Language.LocaleStringResource GetById(int id, bool isCache = true)
        {
            Domain.Entities.Language.LocaleStringResource locale;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalestringresourceKey, "GetById");
                sbKey.AppendFormat("-{0}", id);

                var key = sbKey.ToString();
                locale = _cacheManager.Get<Domain.Entities.Language.LocaleStringResource>(key);
                if (locale == null)
                {
                    locale = _localeStringResourceRepository.GetLocaleStringResourceById(id);
                    _cacheManager.Put(key, locale);
                }
            }
            else
            {
                locale = _localeStringResourceRepository.GetLocaleStringResourceById(id);
            }

            return locale;
        }

        public IEnumerable<Domain.Entities.Language.LocaleStringResource> GetByLanguageId(int languageId, bool isCache = true)
        {
            IEnumerable<Domain.Entities.Language.LocaleStringResource> locale;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalestringresourceKey, "GetByLanguageId");
                sbKey.AppendFormat("-{0}", languageId);

                var key = sbKey.ToString();
                locale = _cacheManager.GetCollection<Domain.Entities.Language.LocaleStringResource>(key);
                if (locale == null)
                {
                    locale = _localeStringResourceRepository.FindBy(
                    x => x.LanguageId == languageId);
                    _cacheManager.Put(key, locale);
                }
            }
            else
            {
                locale = _localeStringResourceRepository.FindBy(
                    x => x.LanguageId == languageId);
            }

            return locale;
        }

        public virtual IQueryable<Domain.Entities.Language.LocaleStringResource> GetAll(int languageId)
        {
            var query = from lsr in _localeStringResourceRepository.Table
                        orderby lsr.ResourceName
                        where lsr.LanguageId == languageId
                        select lsr;

            return query;
        }

        public virtual string GetResource(
          string resourceKey,
          int languageId = 0,
          bool logIfNotFound = true,
          string defaultValue = "",
          bool returnEmptyIfNotFound = false
            ,bool isCache=true)
        {
            Domain.Entities.Language.LocaleStringResource locale;
            var result = string.Empty;

            if (languageId <= 0)
            {
                if (_workContext.WorkingLanguage == null)
                {
                    return defaultValue;
                }

                languageId = _workContext.WorkingLanguage.Id;
            }

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalestringresourceKey, "GetResource");
                sbKey.AppendFormat("-{0}", languageId);

                if (resourceKey.HasValue())
                {
                    sbKey.AppendFormat("-{0}", resourceKey);
                }

                var key = sbKey.ToString();
                locale = _cacheManager.Get<Domain.Entities.Language.LocaleStringResource>(key);
                if (locale == null)
                {
                    locale = GetByName(languageId, resourceKey);
                    _cacheManager.Put(key, locale);
                }
                if (locale != null)
                {
                    result = locale.ResourceValue;
                }
            }
            else
            {
                GetByName(languageId, resourceKey);
            }
          

            return result;
        }
    }
}