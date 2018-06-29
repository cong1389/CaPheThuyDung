using App.Aplication.Extensions;
using App.Core.Caching;
using App.Domain.Common;
using App.Domain.Entities.GlobalSetting;
using App.FakeEntity.Menu;
using App.Front.Extensions;
using App.Front.Models;
using App.Service.Common;
using App.Service.ContactInformation;
using App.Service.Locations;
using App.Service.Menu;
using App.Service.Post;
using App.Service.SeoSetting;
using App.Service.Static;
using App.Service.SystemApp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Front.Controllers
{
    public class SummaryController : FrontBaseController
    {
        private readonly IMenuLinkService _menuLinkService;

        private readonly IProvinceService _provinceService;

        private readonly IDistrictService _districtService;

        private readonly ISystemSettingService _systemSettingService;

        private readonly IContactInfoService _contactInfoService;

        private readonly ISettingSeoGlobalService _settingSeoGlobal;
        private readonly IPostService _postService;
        private readonly IStaticContentService _staticContentService;

        public SummaryController(IMenuLinkService menuLinkService
            , IProvinceService provinceService, IDistrictService districtService, ISystemSettingService systemSettingService
            , IContactInfoService contactInfoService
            , ISettingSeoGlobalService settingSeoGlobal
            , IWorkContext workContext
            , ICacheManager cacheManager, IPostService postService, IStaticContentService staticContentService)
        {
            _menuLinkService = menuLinkService;
            _provinceService = provinceService;
            _districtService = districtService;
            _systemSettingService = systemSettingService;
            _contactInfoService = contactInfoService;
            _settingSeoGlobal = settingSeoGlobal;
            _postService = postService;
            _staticContentService = staticContentService;
        }

        [PartialCache("Long", "*")]
        private List<MenuNavViewModel> CreateMenuNav(int? parentId, IEnumerable<MenuNavViewModel> source)
        {
            return (
                from x in source
                orderby x.OrderDisplay descending
                select x).Where(x =>
                {
                    var nullable1 = x.ParentId;
                    var nullable = parentId;
                    if (nullable1.GetValueOrDefault() != nullable.GetValueOrDefault())
                    {
                        return false;
                    }
                    return nullable1.HasValue == nullable.HasValue;
                }).Select(x => new MenuNavViewModel
                {
                    MenuId = x.MenuId,
                    ParentId = x.ParentId,
                    MenuName = x.MenuName,
                    SeoUrl = x.SeoUrl,
                    OrderDisplay = x.OrderDisplay,
                    ImageBigSize = x.ImageBigSize,
                    CurrentVirtualId = x.CurrentVirtualId,
                    VirtualId = x.VirtualId,
                    ChildNavMenu = CreateMenuNav(x.MenuId, source)
                }).ToList();
        }

        [PartialCache("Long")]
        public ActionResult GetAddressInfo()
        {
            var contactInformation = _contactInfoService.GetTypeAddress((int)TypeAdress.Current);

            var contactInformationLocalize = contactInformation.ToModel();

            return PartialView(contactInformationLocalize);
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult GetCategorySearchBox()
        {
            var menuNavs = new List<MenuNavViewModel>();
            var menuLinks = _menuLinkService.FindBy(x => x.Status == 1 && x.TemplateType == 2, true);
            if (menuLinks.Any())
            {
                var menuNav =
                    from x in menuLinks
                    select new MenuNavViewModel
                    {
                        MenuId = x.Id,
                        ParentId = x.ParentId,
                        MenuName = x.MenuName,
                        SeoUrl = x.SeoUrl,
                        OrderDisplay = x.OrderDisplay,
                        ImageBigSize = x.ImageBigSize,
                        CurrentVirtualId = x.CurrentVirtualId,
                        VirtualId = x.VirtualId
                    };
                menuNavs = CreateMenuNav(null, menuNav);
            }

            return PartialView(menuNavs);
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult GetContactHeader()
        {
            var systemSetting = _systemSettingService.GetEnableOrDisable();

            var systemSettingLocalized = systemSetting.ToModel();

            return PartialView(systemSettingLocalized);
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult GetContactOrder()
        {
            var contactInformation = _contactInfoService.GetTypeAddress((int)TypeAdress.Current);

            var contactInformationLocalized = contactInformation.ToModel();

            return PartialView(contactInformationLocalized);
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ContentResult GetContentFooter()
        {
            var systemSetting = _systemSettingService.GetEnableOrDisable();

            if (systemSetting == null)
            {
                return Content(string.Empty);
            }

            return Content(systemSetting.FooterContent);
        }

        [HttpPost]
        public JsonResult GetDistrictByProvinceId(int provinceId)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var byProvinceId =
                from x in _districtService.GetByProvinceId(provinceId)
                select new { x.Id, x.Name };

            return Json(byProvinceId);
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult Footer()
        {
            var contactInformation = _contactInfoService.GetTypeAddress((int)TypeAdress.Current);

            var contactInformationLocalize = contactInformation.ToModel();

            return PartialView(contactInformationLocalize);
        }

        [PartialCache("Long")]
        public ActionResult GetLogo()
        {
            var systemSetting = GetSystemSettingEnableOrDisableBase();

            var systemSettingLocalized = systemSetting.ToModel();

            return PartialView(systemSettingLocalized);
        }

        [ChildActionOnly]
        [PartialCache("Long")]
        public ActionResult GetMetaTagsSeo()
        {
            var settingSeoGlobal = GetSettingSeoData();

            return PartialView(settingSeoGlobal);
        }

        [ChildActionOnly]
        [PartialCache("Long")]
        public ActionResult GetMeta()
        {
            var systemSetting = _systemSettingService.GetEnableOrDisable();

            if (systemSetting == null)
            {
                return HttpNotFound();
            }

            var systemSettingLocalized = systemSetting.ToModel();

            var controller = Request.RequestContext.RouteData.Values["Controller"].ToString();
            var action = Request.RequestContext.RouteData.Values["Action"].ToString();

            if (controller.Equals("Home") && action.Equals("Index"))
            {
                ViewBag.Title = systemSettingLocalized.Title;
                ViewBag.KeyWords = systemSettingLocalized.MetaKeywords;
                ViewBag.SiteUrl = Url.Action("Index", "Home", new { area = "" });
                ViewBag.Description = systemSettingLocalized.Description;
                ViewBag.Image = Url.Content(string.Concat("~/", systemSettingLocalized.LogoImage));
            }

            ViewBag.Favicon = Url.Content(string.Concat("~/", systemSettingLocalized.FaviconImage));

            return PartialView(systemSettingLocalized);
        }

        [ChildActionOnly]
        [PartialCache("Short")]
        public ActionResult GetProvinceSearchBox()
        {
            var provinces = _provinceService.FindBy(x => x.Status == 1);

            return PartialView(provinces);
        }

        [ChildActionOnly]
        [PartialCache("Long")]
        public ActionResult ScriptSippet()
        {
            var settingSeoGlobal = GetSettingSeoData();

            return PartialView(settingSeoGlobal);
        }

        [PartialCache("Long")]
        public JsonResult GetPostAddress()
        {
            var contactInformation = _contactInfoService.GetTypeAddress((int)TypeAdress.Current);

            var contactInformationLocalize = contactInformation.ToModel();

            var jsonResult =
                Json(
                    new
                    {
                        success = true,
                        list = this.RenderRazorViewToString("_Post.Address", contactInformationLocalize)
                    }, JsonRequestBehavior.AllowGet);

            return jsonResult;

        }

        [PartialCache("Long")]
        public JsonResult GetFooterAddress()
        {
            //var contactInformation = _contactInfoService.GetEnableOrDisables();
            //var contactInformation = _contactInfoService.FindBy(x => x.Status == 1, true);

            var contactInformationLocalize = GetBaseContactInfoEnableOrDisables();

            var jsonResult =
                Json(
                    new
                    {
                        success = true,
                        list = this.RenderRazorViewToString("_Footer.Address", contactInformationLocalize)
                    }, JsonRequestBehavior.AllowGet);

            return jsonResult;

        }

        [PartialCache("Long")]
        public JsonResult GetFooterLogo()
        {
            var systemSettingLocalize = GetSystemSettingEnableOrDisableBase();

            var jsonResult =
                Json(new {success = true, list = this.RenderRazorViewToString("_Footer.Logo", systemSettingLocalize)},
                    JsonRequestBehavior.AllowGet);

            return jsonResult;

        }

        [PartialCache("Long")]
        public JsonResult GetSystemSetting()
        {
            var systemSettingLocalize = GetSystemSettingEnableOrDisableBase();// _systemSettingService.Get(x => x.Status == 1);
            
            var jsonResult = Json(new { success = true, list = systemSettingLocalize }, JsonRequestBehavior.AllowGet);

            return jsonResult;

        }

        #region Social

        public async Task<JsonResult> GetSettingSeo()
        {
            var settingSeoGlobal = await Task.FromResult(GetSettingSeoData());

            var jsonResult = Json(new { success = true, response = settingSeoGlobal }, JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        #endregion

        #region System Setting

        [PartialCache("Long")]
        public JsonResult GetLogoMobile()
        {
            var systemSettingLocalize = GetSystemSettingEnableOrDisableBase();

            var jsonResult = Json(new { success = true, list = systemSettingLocalize }, JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        private SystemSetting GetSystemSettingEnableOrDisableBase()
        {
            var systemSetting = _systemSettingService.GetEnableOrDisable();
            var systemSettingLocalize = systemSetting.ToModel();

            return systemSettingLocalize;
        }

        private IEnumerable<ContactInformation> GetBaseContactInfoEnableOrDisables()
        {
            var contactInformations = _contactInfoService.GetEnableOrDisables();
            var contactInformationsLocalize = contactInformations.Select(x => x.ToModel());

            return contactInformationsLocalize;
        }

        #endregion

        #region Setting Seo

        private SettingSeoGlobal GetSettingSeoData()
        {
            return _settingSeoGlobal.GetEnableOrDisable(); 
        }

        #endregion

        //Hien thi san pham footer
        [PartialCache("Medium")]
        public JsonResult PostOutOfStock()
        {
            var post = _postService.GetTop(6, x => x.Status == (int)Status.Enable && x.OutOfStock);

            var jsonResult = Json(new { success = true, list = this.RenderRazorViewToString("_Footer.Product", post) },
                JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        [PartialCache("Long")]
        public JsonResult GetIntro(int menuId)
        {
            var staticContent = _staticContentService.Get(x => x.Id == menuId, true);

            var staticContentLocalized = staticContent.ToModel();

            var jsonResult = Json(new { success = true, list = this.RenderRazorViewToString("_Footer.Intro", staticContentLocalized) },
                JsonRequestBehavior.AllowGet);

            return jsonResult;
        }
    }
}