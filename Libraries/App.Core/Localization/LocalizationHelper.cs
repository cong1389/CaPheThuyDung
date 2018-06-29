using App.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace App.Core.Localization
{
    public static class LocalizationHelper
    {
        private static readonly HashSet<string> CultureCodes =
            new HashSet<string>(
                CultureInfo.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures | CultureTypes.UserCustomCulture)
                .Select(x => x.IetfLanguageTag)
                .Where(x => !string.IsNullOrWhiteSpace(x)), StringComparer.OrdinalIgnoreCase);

        public static bool IsValidCultureCode(string locale)
        {
            return locale.HasValue() && CultureCodes.Contains(locale);
        }

        /// <summary>
        /// Enumerates all parent cultures, excluding the top-most invariant culture
        /// </summary>
        /// <param name="locale">The ISO culture code, e.g. de-DE, en-US or just en</param>
        /// <returns>Parent cultures</returns>
        public static IEnumerable<CultureInfo> EnumerateParentCultures(string locale)
        {
            if (locale.IsEmpty() || !CultureCodes.Contains(locale))
            {
                return Enumerable.Empty<CultureInfo>();
            }

            return EnumerateParentCultures(CultureInfo.GetCultureInfo(locale));
        }

        /// <summary>
        /// Enumerates all parent cultures, excluding the top-most invariant culture
        /// </summary>
        /// <param name="culture">The culture info to enumerate parents for</param>
        /// <returns>Parent cultures</returns>
        public static IEnumerable<CultureInfo> EnumerateParentCultures(CultureInfo culture)
        {
            if (culture == null)
            {
                yield break;
            }

            while (culture.Parent.TwoLetterISOLanguageName != "iv")
            {
                yield return culture.Parent;
                culture = culture.Parent;
            }
        }

        public static string GetLanguageNativeName(string locale)
        {
            try
            {
                if (locale.HasValue())
                {
                    var info = CultureInfo.GetCultureInfoByIetfLanguageTag(locale);
                    return info.NativeName;
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }

        public static string GetCurrencySymbol(string locale)
        {
            try
            {
                if (locale.HasValue())
                {
                    var info = new RegionInfo(locale);
                    return info.CurrencySymbol;
                }
            }
            catch
            {
                // ignored
            }

            return null;
        }
    }
}
