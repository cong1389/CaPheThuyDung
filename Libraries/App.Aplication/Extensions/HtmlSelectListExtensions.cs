using App.Service.Common;
using App.Service.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Extensions
{
    public static class HtmlSelectListExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, bool markCurrentAsSelected = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("An Enumeration type is required.", "enumObj");

            var localizationService = DependencyResolver.Current.GetService<ILanguageService>();
            var workContext = DependencyResolver.Current.GetService<IWorkContext>();

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         select new { ID = Convert.ToInt32(enumValue), Name = enumValue.GetLocalizedEnum(localizationService, workContext) };

            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);

            return new SelectList(values, "ID", "Name", selectedValue);
        }

    }
}
