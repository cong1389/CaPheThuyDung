using System.Web.Mvc;
using App.Domain.Common;
using App.Front.Models;
using App.Service.Common;
using App.Service.Language;

namespace App.Front.Controllers
{
    public class CommonController : FrontBaseController
    {
        private readonly ICommonServices _services;

        private readonly ILanguageService _language;

        public CommonController(ICommonServices services, ILanguageService language)
        {
            _services = services;
            _language = language;
        }

        [PartialCache("Medium","*")]
        public ActionResult SetLanguage(int langid, string returnUrl = "")
        {
            var language = _language.GetById(langid);

            if (language != null && language.Status == (int)Status.Enable)
            {
                _services.WorkContext.WorkingLanguage = language;
            }

            return Redirect(returnUrl);
        }

    }
}