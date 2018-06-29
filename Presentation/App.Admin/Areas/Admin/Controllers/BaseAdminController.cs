using App.Domain.Common;
using App.Service.Language;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
	public class BaseAdminController : Controller
	{
        protected virtual void AddLocales<TLocalizedPropertyViewModelLocal>(ILanguageService languageService,
	        IList<TLocalizedPropertyViewModelLocal> locales) where TLocalizedPropertyViewModelLocal : ILocalizedModelLocal
        {
            AddLocales(languageService, locales, null);
        }

        protected virtual void AddLocales<TLocalizedPropertyViewModelLocal>(ILanguageService languageService,
	        IList<TLocalizedPropertyViewModelLocal> locales, Action<TLocalizedPropertyViewModelLocal, int> configure)
	        where TLocalizedPropertyViewModelLocal : ILocalizedModelLocal
        {
            foreach (var language in languageService.FindBy(x => x.Status == (int)Status.Enable))
            {
                var locale = Activator.CreateInstance<TLocalizedPropertyViewModelLocal>();
                locale.LanguageId = language.Id;
	            configure?.Invoke(locale, locale.LanguageId);
	            locales.Add(locale);
            }
        }

        public int PageSize => int.Parse(ConfigurationManager.AppSettings["ItemsPerPage"] ?? "10");

	}
}