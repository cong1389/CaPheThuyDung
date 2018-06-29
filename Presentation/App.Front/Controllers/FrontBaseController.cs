using System;
using System.Collections.Generic;
using System.Web.Mvc;
using App.Core.Localization;
using App.Front.Models;
using App.Service.Language;

namespace App.Front.Controllers
{
    public  class FrontBaseController : Controller
	{
        [PartialCache("Long")]
        protected virtual void AddLocales<TLocalizedPropertyViewModelLocal>(ILanguageService languageService,
            IList<TLocalizedPropertyViewModelLocal> locales)
            where TLocalizedPropertyViewModelLocal : ILocalizedModelLocal
        {
            AddLocales(languageService, locales, null);
        }

        [PartialCache("Long","*")]
        protected virtual void AddLocales<TLocalizedPropertyViewModelLocal>(ILanguageService languageService,
            IList<TLocalizedPropertyViewModelLocal> locales, Action<TLocalizedPropertyViewModelLocal, int> configure)
            where TLocalizedPropertyViewModelLocal : ILocalizedModelLocal
        {
            foreach (var language in languageService.GetAll())
            {
                var locale = Activator.CreateInstance<TLocalizedPropertyViewModelLocal>();
                locale.LanguageId = language.Id;
                configure?.Invoke(locale, locale.LanguageId);
                locales.Add(locale);
            }
        }

	    protected int PageSize => 20;

	    public FrontBaseController()
		{
            T = NullLocalizer.Instance;
        }

	    protected Localizer T
        {
            get;
            set;
        }
    }
}