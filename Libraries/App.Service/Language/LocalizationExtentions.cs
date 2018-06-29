using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using App.Core.Caching;
using App.Service.Common;
using App.Service.LocalizedProperty;

namespace App.Service.Language
{
    public static class LocalizationExtentions
    {
        private const string CacheLocalizaKey = "db.Localization.{0}";
       
        public static string GetLocalized<T>(this T entity, Expression<Func<T, string>> keySelector, int entityId)
        {
            var workContext = DependencyResolver.Current.GetService<IWorkContext>();
            return GetLocalized(entity, keySelector, entityId, workContext.WorkingLanguage.Id);
        }

        /// <summary>
        /// Get localized property of an entity
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Localized property</returns>
        public static string GetLocalized<T>(this T entity,
            Expression<Func<T, string>> keySelector, int entityId, int languageId,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)

        {
            return GetLocalized<T, string>(entity, keySelector, entityId, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
        }

        public static string GetLocalized<T, TPropType>(this T entity,
           Expression<Func<T, TPropType>> keySelector,
           int entityId,
           int languageId,
           bool returnDefaultValue = true,
           bool ensureTwoPublishedLanguages = true)

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
            string result = null;

            // load localized value
            var localeKeyGroup = typeof(T).Name.Replace("ViewModel", "");
            var localeKey = propInfo.Name;

            if (languageId > 0)
            {
                DependencyResolver.Current.GetService<ICacheManager>();
                var localizedPropertyService = DependencyResolver.Current.GetService<ILocalizedPropertyService>();

                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalizaKey, "GetLocalized");
                var guid = entity.GetType().GUID;
                sbKey.AppendFormat("-{0}", guid);
                sbKey.AppendFormat("-{0}", entityId);
                sbKey.AppendFormat("-{0}", languageId);
                sbKey.AppendFormat("-{0}", localeKeyGroup);
                sbKey.AppendFormat("-{0}", localeKey);
                
                var localizedProperty = localizedPropertyService.GetByKey(languageId
                    , entityId, localeKeyGroup, localeKey);

                result = localizedProperty?.LocaleValue;
            }

            return result;
        }

        /// <summary>
        /// GetLocalizedByLocaleKey
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Class name</param>
        /// <param name="fallBackValue">Value mặc định nếu không có localized</param>
        /// <param name="entityId">Id của thực thể cần set localized</param>
        /// <param name="languageId">Id ngôn ngữ</param>
        /// <param name="localeKeyGroup">Tên nhóm</param>
        /// <param name="localeKey">Tên key</param>
        /// <returns></returns>
        public static string GetLocalizedByLocaleKey<T>(this T entity, string fallBackValue, int entityId, int languageId, string localeKeyGroup, string localeKey)
        {
            string result = null;
            if (languageId > 0)
            {
                var cacheManager = DependencyResolver.Current.GetService<ICacheManager>();
                var localizedPropertyService = DependencyResolver.Current.GetService<ILocalizedPropertyService>();

                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheLocalizaKey, "GetLocalizedByLocaleKey");
                var guid = entity.GetType().GUID;
                sbKey.AppendFormat("-{0}", guid);
                sbKey.AppendFormat("-{0}", entityId);
                sbKey.AppendFormat("-{0}", languageId);
                sbKey.AppendFormat("-{0}", localeKeyGroup);
                sbKey.AppendFormat("-{0}", localeKey);

                var key = sbKey.ToString();
                var localizedProperty = cacheManager.Get<Domain.Entities.Language.LocalizedProperty>(key);
                if (localizedProperty == null)
                {
                    localizedProperty = localizedPropertyService.GetByKey(languageId
                    , entityId, localeKeyGroup, localeKey);
                    cacheManager.Put(key, localizedProperty);
                }                
              
                result = localizedProperty != null ? localizedProperty.LocaleValue : fallBackValue;
            }

            return result;
        }

    }
}
