using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using App.Core.Caching;
using App.Service.Language;

namespace App.Admin.Controllers
{
    public class BaseAdminUploadController : Controller
    {
	    protected virtual void AddLocales<TLocalizedPropertyViewModelLocal>(ILanguageService languageService, IList<TLocalizedPropertyViewModelLocal> locales) where TLocalizedPropertyViewModelLocal : ILocalizedModelLocal
        {
            AddLocales(languageService, locales, null);
        }

        protected virtual void AddLocales<TLocalizedPropertyViewModelLocal>(ILanguageService languageService, IList<TLocalizedPropertyViewModelLocal> locales, Action<TLocalizedPropertyViewModelLocal, int> configure) where TLocalizedPropertyViewModelLocal : ILocalizedModelLocal
        {
            foreach (var language in languageService.FindBy(x => x.Status == 1))
            {
                var locale = Activator.CreateInstance<TLocalizedPropertyViewModelLocal>();
                locale.LanguageId = language.Id;
                configure?.Invoke(locale, locale.LanguageId);
                locales.Add(locale);
            }
        }

        public int PageSize => int.Parse(ConfigurationManager.AppSettings["ItemsPerPage"] ?? "10");

	    protected string Key
        {
            get => ViewData["Key"].ToString();
		    set => ViewData["Key"] = value;
	    }

        public BaseAdminUploadController(ICacheManager cacheManager)
        {
	        cacheManager.Clear();
            //ICacheManager ICacheManager = DependencyResolver.Current.GetService<ICacheManager>();
        }
    }
}