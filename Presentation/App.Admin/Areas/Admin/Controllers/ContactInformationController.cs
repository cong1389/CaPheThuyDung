using App.Admin.Helpers;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.FakeEntity.ContactInformation;
using App.Framework.Utilities;
using App.Service.ContactInformation;
using App.Service.Language;
using App.Service.LocalizedProperty;
using App.Service.Locations;
using AutoMapper;
using Resources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class ContactInformationController : BaseAdminController
    {
        private const string CacheContactinfoKey = "db.ContactInfo";

	    private readonly IContactInfoService _contactInfoService;

        private readonly IProvinceService _provinceService;

        private readonly ILanguageService _languageService;

        private readonly ILocalizedPropertyService _localizedPropertyService;

        public ContactInformationController(
            IContactInfoService contactInfoService
            , IProvinceService provinceService
            , ILanguageService languageService
            , ILocalizedPropertyService localizedPropertyService
            , ICacheManager cacheManager
            )
        {
	        _contactInfoService = contactInfoService;
            _provinceService = provinceService;
            _languageService = languageService;
            _localizedPropertyService = localizedPropertyService;

	        //Clear cache
            cacheManager.RemoveByPattern(CacheContactinfoKey);
        }

        [RequiredPermisson(Roles = "CreateEditContactInformation")]
        public ActionResult Create()
        {
            var model = new ContactInformationViewModel();

            //Add locales to model
            AddLocales(_languageService, model.Locales);

            return View(model);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditContactInformation")]
        public ActionResult Create(ContactInformationViewModel model, string returnUrl)
        {
            ActionResult action;
            try
            {
                if (!ModelState.IsValid)
                {
                    var messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                          .Select(v => v.ErrorMessage + " " + v.Exception));
                    ModelState.AddModelError("", messages);
                    return View(model);
                }

                var modelMap = Mapper.Map<ContactInformationViewModel, ContactInformation>(model);
                _contactInfoService.Create(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Title, localized.Title, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Address, localized.Address, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.ContactInformation)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("MailSetting.Create: ", exception.Message));
                return View(model);
            }
            return action;
        }

        [RequiredPermisson(Roles = "DeleteContactInformation")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var contactInformations =
                        from id in ids
                        select _contactInfoService.GetById(int.Parse(id));
                    _contactInfoService.BatchDelete(contactInformations);

                    //Delete localize
                    for (var i = 0; i < ids.Length; i++)
                    {
                        var ieLocalizedProperty
                           = _localizedPropertyService.GetByEntityId(int.Parse(ids[i]));
                        _localizedPropertyService.BatchDelete(ieLocalizedProperty);
                    }
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("ContactInformation.Delete: ", exception.Message));
            }
            return RedirectToAction("Index");
        }

        [RequiredPermisson(Roles = "CreateEditContactInformation")]
        public ActionResult Edit(int id)
        {
            var modelMap = Mapper.Map<ContactInformation, ContactInformationViewModel>(_contactInfoService.GetById(id));

            //Add Locales to model
            AddLocales(_languageService, modelMap.Locales, (locale, languageId) =>
            {
                locale.Id = modelMap.Id;
                locale.Language = modelMap.Language;
                locale.Title = modelMap.GetLocalized(x => x.Title, id, languageId, false, false);
                locale.Lag = modelMap.Lag;
                locale.Lat = modelMap.Lat;
                locale.Type = modelMap.Type;
                locale.OrderDisplay = modelMap.OrderDisplay;
                locale.Status = modelMap.Status;
                locale.Email = modelMap.Email;
                locale.Hotline = modelMap.Hotline;
                locale.MobilePhone = modelMap.MobilePhone;
                locale.Address = modelMap.GetLocalized(x => x.Address, id, languageId, false, false);
                locale.Fax = modelMap.Fax;
                locale.NumberOfStore = modelMap.NumberOfStore;
                locale.ProvinceId = modelMap.ProvinceId;
            });

            return View(modelMap);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditContactInformation")]
        public ActionResult Edit(ContactInformationViewModel model, string returnUrl)
        {
            ActionResult action;
            try
            {
                if (!ModelState.IsValid)
                {
                    var messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                          .Select(v => v.ErrorMessage + " " + v.Exception));
                    ModelState.AddModelError("", messages);
                    return View(model);
                }

                var modelMap = Mapper.Map<ContactInformationViewModel, ContactInformation>(model);
                _contactInfoService.Update(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Title, localized.Title, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Address, localized.Address, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.ContactInformation)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("MailSetting.Create: ", exception.Message));
                return View(model);
            }
            return action;
        }

        [RequiredPermisson(Roles = "ViewContactInformation")]
        public ActionResult Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;
            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords,
                Sorts = new SortBuilder
                {
                    ColumnName = "Title",
                    ColumnOrder = SortBuilder.SortOrder.Descending
                }
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };
            var contactInformations = _contactInfoService.PagedList(sortingPagingBuilder, paging);
            if (contactInformations != null && contactInformations.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }
            return View(contactInformations);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.RouteData.Values["action"].Equals("create") || filterContext.RouteData.Values["action"].Equals("edit"))
            {
                var all = _provinceService.GetAll();
                ViewBag.Provinces = all;
            }
        }
    }
}