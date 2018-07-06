using System.Web.Mvc;
using App.Front.Extensions;
using App.Service.SystemApp;

namespace App.Front.Controllers
{
    public class HomeController : FrontBaseController
    {
        private readonly ISystemSettingService _systemSettingService;

        public HomeController(ISystemSettingService systemSettingService)
        {
            _systemSettingService = systemSettingService;
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult UnderConstruction()
        {
            return View();
        }

        [OutputCache(CacheProfile = "Medium")]
        public ActionResult Index()
        {
            var systemSetting = _systemSettingService.GetEnableOrDisable();
            if (systemSetting != null)
            {
                var systemSettingLocalize = systemSetting.ToModel();
                ViewBag.Title = systemSettingLocalize.MetaTitle ?? systemSettingLocalize.Title;
                ViewBag.KeyWords = systemSettingLocalize.MetaKeywords;
                ViewBag.SiteUrl = Url.Action("Index", "Home", new { area = "" });
                ViewBag.Description = systemSettingLocalize.Description;
                ViewBag.Image = Url.Content(string.Concat("~/", systemSettingLocalize.LogoImage));
                ViewBag.Favicon = Url.Content(string.Concat("~/", systemSettingLocalize.FaviconImage));

                if (systemSettingLocalize.MaintanceSite && Aplication.Utils.IsHomePage() && !HttpContext.Request.RawUrl.Contains("prepare-index.html"))
                {
                    return RedirectToAction("UnderConstruction");
                }
                else
                {
                    RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}