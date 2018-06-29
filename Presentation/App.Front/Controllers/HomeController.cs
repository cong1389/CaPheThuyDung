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
            var systemSettingLocalize = systemSetting.ToModel();

            //var systemSetting = _systemSettingService.Get(x => x.Status == 1, true);
            if (systemSettingLocalize != null)
            {
                ViewBag.Title = systemSettingLocalize.MetaTitle ?? systemSettingLocalize.Title;
                ViewBag.KeyWords = systemSettingLocalize.MetaKeywords;
                ViewBag.SiteUrl = Url.Action("Index", "Home", new { area = "" });
                ViewBag.Description = systemSettingLocalize.Description;
                ViewBag.Image = Url.Content(string.Concat("~/", systemSettingLocalize.LogoImage));
                ViewBag.Favicon = Url.Content(string.Concat("~/", systemSettingLocalize.FaviconImage));

                if (systemSettingLocalize.MaintanceSite)
                {
                    return RedirectToAction("UnderConstruction");
                }
            }

            return View();
        }

    }
}