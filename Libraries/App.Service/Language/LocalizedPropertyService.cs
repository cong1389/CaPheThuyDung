using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using App.Core.Caching;
using App.Core.Common;
using App.Core.Extensions;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Language;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.LocalizedProperty
{
    public class LocalizedPropertyService : BaseService<Domain.Entities.Language.LocalizedProperty>, ILocalizedPropertyService
    {
        private const string CacheLocalizedpropertyKey = "db.LocalizedProperty.{0}";

        private readonly ICacheManager _cacheManager;

        private readonly ILocalizedPropertyRepository _localizedPropertyRepository;

        private readonly IUnitOfWork _unitOfWork;

        public LocalizedPropertyService(IUnitOfWork unitOfWork,
            ILocalizedPropertyRepository localizedPropertyRepository, ICacheManager cacheManager) : base(unitOfWork,
            localizedPropertyRepository)
        {
            _unitOfWork = unitOfWork;
            _localizedPropertyRepository = localizedPropertyRepository;
            _cacheManager = cacheManager;
        }

        public void CreateLocalizedProperty(Domain.Entities.Language.LocalizedProperty localizedProperty)
        {
            _localizedPropertyRepository.Add(localizedProperty);
        }

        public Domain.Entities.Language.LocalizedProperty GetById(int id, bool isCache = true)
        {
            Domain.Entities.Language.LocalizedProperty localizedProperty;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalizedpropertyKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                localizedProperty = _cacheManager.Get<Domain.Entities.Language.LocalizedProperty>(key);
                if (localizedProperty == null)
                {
                    localizedProperty = _localizedPropertyRepository.GetId(id);
                    _cacheManager.Put(key, localizedProperty);
                }
            }
            else
            {
                localizedProperty = _localizedPropertyRepository.GetId(id);
            }

            return localizedProperty;
        }

        public IEnumerable<Domain.Entities.Language.LocalizedProperty> GetByEntityId(int entityId, bool isCache = true)
        {
            IEnumerable<Domain.Entities.Language.LocalizedProperty> localizedProperty;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalizedpropertyKey, "GetByEntityId");
                sbKey.Append(entityId);

                var key = sbKey.ToString();
                localizedProperty = _cacheManager.GetCollection<Domain.Entities.Language.LocalizedProperty>(key);
                if (localizedProperty == null)
                {
                    localizedProperty = _localizedPropertyRepository.FindBy(x => x.EntityId == entityId);
                    _cacheManager.Put(key, localizedProperty);
                }
            }
            else
            {
                localizedProperty = _localizedPropertyRepository.FindBy(x => x.EntityId == entityId);
            }


            return localizedProperty;
        }

        public Domain.Entities.Language.LocalizedProperty GetByKey(int languageId, int entityId, string localeKeyGroup, string localeKey, bool isCache = true)
        {
            Domain.Entities.Language.LocalizedProperty attr;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalizedpropertyKey, "GetByKey");
                sbKey.Append(languageId);
                sbKey.Append(entityId);

                if (localeKeyGroup.HasValue())
                {
                    sbKey.AppendFormat("-{0}", localeKeyGroup);
                }

                if (localeKey.HasValue())
                {
                    sbKey.AppendFormat("-{0}", localeKey);
                }

                var key = sbKey.ToString();
                attr = _cacheManager.Get<Domain.Entities.Language.LocalizedProperty>(key);
                if (attr == null)
                {
                    attr = Get(x =>
                      x.LanguageId.Equals(languageId)
                      && x.EntityId.Equals(entityId)
                       && x.LocaleKeyGroup.Equals(localeKeyGroup)
                       && x.LocaleKey.Equals(localeKey));
                    _cacheManager.Put(key, attr);
                }
            }
            else
            {
                attr = Get(x =>
                      x.LanguageId.Equals(languageId)
                      && x.EntityId.Equals(entityId)
                       && x.LocaleKeyGroup.Equals(localeKeyGroup)
                       && x.LocaleKey.Equals(localeKey));
            }

            return attr;
        }

        public IEnumerable<Domain.Entities.Language.LocalizedProperty> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _localizedPropertyRepository.PagedSearchList(sortbuBuilder, page);
        }

        public int SaveLocalizedProperty()
        {
            return _unitOfWork.Commit();
        }

        public virtual void SaveLocalizedValue<T>(
            T entity,
            Expression<Func<T, string>> keySelector,
            string localeValue,
            int languageId) where T : AuditableEntity<int>
        {
            SaveLocalizedValueItem(entity, keySelector, localeValue, languageId);
        }

        public virtual void SaveLocalizedValueItem<T, TPropType>(
           T entity,
           Expression<Func<T, TPropType>> keySelector,
           string localeValue,
           int languageId) where T : AuditableEntity<int>
        {
            if (!(keySelector.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");
            }

            var keyGroup = typeof(T).Name;
            var key = propInfo.Name;

            var obj = GetByKey(languageId, entity.Id
           , keyGroup, key);

            if (obj == null)
            {
                if (!string.IsNullOrEmpty(localeValue))
                {
                    obj = new Domain.Entities.Language.LocalizedProperty
                    {
                        EntityId = entity.Id,
                        LanguageId = languageId,
                        LocaleKey = key,
                        LocaleKeyGroup = keyGroup,
                        LocaleValue = localeValue
                    };
                    Create(obj);
                }
            }
            else
            {
                obj.Id = obj.Id;
                obj.EntityId = entity.Id;
                obj.LanguageId = languageId;
                obj.LocaleKey = key;
                obj.LocaleValue = localeValue;
                Update(obj);
            }
        }

    }
}