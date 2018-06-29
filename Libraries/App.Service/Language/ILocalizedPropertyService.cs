using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Core.Common;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.LocalizedProperty
{
    public interface ILocalizedPropertyService : IBaseService<Domain.Entities.Language.LocalizedProperty>
    {
        void CreateLocalizedProperty(Domain.Entities.Language.LocalizedProperty localizedProperty);

        Domain.Entities.Language.LocalizedProperty GetById(int id, bool isCache = true);

        IEnumerable<Domain.Entities.Language.LocalizedProperty> GetByEntityId(int entityId, bool isCache = true);

        Domain.Entities.Language.LocalizedProperty GetByKey(int languageId,int entityId, string localeKeyGroup, string localeKey, bool isCache = true);

        IEnumerable<Domain.Entities.Language.LocalizedProperty> PagedList(SortingPagingBuilder sortBuider, Paging page);

        int SaveLocalizedProperty();

        void SaveLocalizedValue<T>(
            T entity,
            Expression<Func<T, string>> keySelector,
            string localeValue,
            int languageId) where T : AuditableEntity<int>;

        void SaveLocalizedValueItem<T, TPropType>(
           T entity,
           Expression<Func<T, TPropType>> keySelector,
           string localeValue,
           int languageId) where T : AuditableEntity<int>;
        
    }
}