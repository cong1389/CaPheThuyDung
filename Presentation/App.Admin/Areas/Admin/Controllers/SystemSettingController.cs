using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Admin.Helpers;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.FakeEntity.System;
using App.Framework.Utilities;
using App.Service.Language;
using App.Service.LocalizedProperty;
using App.Service.SystemApp;
using AutoMapper;
using Resources;
using static System.String;

namespace App.Admin.Controllers
{
    public class SystemSettingController : BaseAdminController
    {
        private const string CacheSystemsettingKey = "db.SystemSetting";

	    private readonly ISystemSettingService _systemSettingService;

        private readonly ILanguageService _languageService;

        private readonly ILocalizedPropertyService _localizedPropertyService;

        public SystemSettingController(
            ISystemSettingService systemSettingService
            , ILanguageService languageService
            , ILocalizedPropertyService localizedPropertyService
             , ICacheManager cacheManager
            )
        {
	        _systemSettingService = systemSettingService;
            _languageService = languageService;
            _localizedPropertyService = localizedPropertyService;

	        //Clear cache
            cacheManager.RemoveByPattern(CacheSystemsettingKey);

        }

        [RequiredPermisson(Roles = "CreateEditSystemSetting")]
        public ActionResult Create()
        {
            var model = new SystemSettingViewModel
            {
                Status = 1
            };

            //Add locales to model
            AddLocales(_languageService, model.Locales);

            return View(model);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditSystemSetting")]
        public ActionResult Create(SystemSettingViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var messages = Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                         .Select(v => v.ErrorMessage + " " + v.Exception));
                    ModelState.AddModelError("", messages);
                    return View(model);
                }

                if (model.Status == 1)
                {
                    var systemSettings = _systemSettingService.FindBy(x => x.Status == 1);
                    if (systemSettings.IsAny())
                    {
                        foreach (var item in systemSettings)
                        {
                            item.Status = 0;
                            _systemSettingService.Update(item);
                        }
                    }
                }

                if (model.Favicon != null && model.Favicon.ContentLength > 0)
                {
                    var extension = Path.GetExtension(model.Favicon.FileName);
                    var fileName = Concat("favicon", extension);
                    var path = Path.Combine(Server.MapPath(Concat("~/", Contains.SystemSettingFolder)), fileName);

                    //Check and delete logo exists
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    model.Favicon.SaveAs(path);
                    model.FaviconImage = Concat(Contains.SystemSettingFolder, fileName);
                }

                if (model.Logo != null && model.Logo.ContentLength > 0)
                {
                    var extension = Path.GetExtension(model.Logo.FileName);
                    var fileName = Concat("logo", extension);
                    var path = Path.Combine(Server.MapPath(Concat("~/", Contains.SystemSettingFolder)), fileName);

                    //Check and delete logo exists
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    model.Logo.SaveAs(path);
                    model.LogoImage = Concat(Contains.SystemSettingFolder, fileName);
                }

                if (model.LogoFooter != null && model.LogoFooter.ContentLength > 0)
                {
                    var extension = Path.GetExtension(model.LogoFooter.FileName);
                    var fileName = Concat("logoFooter", extension);
                    var path = Path.Combine(Server.MapPath(Concat("~/", Contains.SystemSettingFolder)), fileName);

                    //Check and delete logo exists
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    model.LogoFooter.SaveAs(path);
                    model.LogoFooterImage = Concat(Contains.SystemSettingFolder, fileName);
                }

                var modelMap = Mapper.Map<SystemSettingViewModel, SystemSetting>(model);
                _systemSettingService.Create(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Title, localized.Title, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.FooterContent, localized.FooterContent, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaTitle, localized.MetaTitle, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaKeywords, localized.MetaKeywords, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaDescription, localized.MetaDescription, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.TimeWork, localized.TimeWork, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Slogan, localized.Slogan, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", Format(MessageUI.CreateSuccess, FormUI.SystemSetting)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    return RedirectToAction("Index");
                }

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(Concat("SystemSetting.Create: ", ex.Message));
                ModelState.AddModelError("", ex.Message);

                return View(model);
            }
        }

        [RequiredPermisson(Roles = "DeleteSystemSetting")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var systemSettings =
                        from id in ids
                        select _systemSettingService.GetById(int.Parse(id));
                    _systemSettingService.BatchDelete(systemSettings);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(Concat("SystemSetting.Delete: ", ex.Message));
            }

            return RedirectToAction("Index");
        }

        [RequiredPermisson(Roles = "CreateEditSystemSetting")]
        public ActionResult Edit(int id)
        {
            var modelMap = Mapper.Map<SystemSetting, SystemSettingViewModel>(_systemSettingService.GetById(id));

            //Add Locales to model
            AddLocales(_languageService, modelMap.Locales, (locale, languageId) =>
            {
                locale.Id = modelMap.Id;
                locale.LocalesId = modelMap.Id;
                locale.Language = modelMap.Language;
                locale.Status = modelMap.Status;
                locale.FaviconImage = modelMap.FaviconImage;
                locale.LogoImage = modelMap.LogoImage;
                locale.LogoFooterImage = modelMap.LogoFooterImage;
                locale.MaintanceSite = modelMap.MaintanceSite;
                locale.Hotline = modelMap.Hotline;
                locale.Email = modelMap.Email;
                locale.TimeWork = modelMap.TimeWork;

                locale.Title = modelMap.GetLocalized(x => x.Title, id, languageId, false, false);
                locale.FooterContent = modelMap.GetLocalized(x => x.FooterContent, id, languageId, false, false);
                locale.Description = modelMap.GetLocalized(x => x.Description, id, languageId, false, false);
                locale.MetaTitle = modelMap.GetLocalized(x => x.MetaTitle, id, languageId, false, false);
                locale.MetaKeywords = modelMap.GetLocalized(x => x.MetaKeywords, id, languageId, false, false);
                locale.MetaDescription = modelMap.GetLocalized(x => x.MetaDescription, id, languageId, false, false);
                locale.Slogan = modelMap.GetLocalized(x => x.Slogan, id, languageId, false, false);
            });

            return View(modelMap);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditSystemSetting")]
        public ActionResult Edit(SystemSettingViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var messages = Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                        .Select(v => v.ErrorMessage + " " + v.Exception));
                    ModelState.AddModelError("", messages);

                    return View(model);
                }

                var byId = _systemSettingService.GetById(model.Id, false);
                if (model.Status == 1 && model.Status != byId.Status)
                {
                    var systemSettings = _systemSettingService.FindBy(x => x.Status == 1);
                    if (systemSettings.IsAny())
                    {
                        foreach (var systemSetting1 in systemSettings)
                        {
                            systemSetting1.Status = 0;
                            _systemSettingService.Update(systemSetting1);
                        }
                    }
                }

                if (model.Favicon != null && model.Favicon.ContentLength > 0)
                {
                    var extension = Path.GetExtension(model.Favicon.FileName);
                    var fileName = Concat("favicon", extension);
                    var path = Path.Combine(Server.MapPath(Concat("~/", Contains.SystemSettingFolder)), fileName);

                    //Check and delete logo exists
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    model.Favicon.SaveAs(path);
                    model.FaviconImage = Concat(Contains.SystemSettingFolder, fileName);
                }

                if (model.Logo != null && model.Logo.ContentLength > 0)
                {
                    var extension = Path.GetExtension(model.Logo.FileName);
                    var fileName = Concat("logo", extension);
                    var path = Path.Combine(Server.MapPath(Concat("~/", Contains.SystemSettingFolder)), fileName);

                    //Check and delete logo exists
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    model.Logo.SaveAs(path);
                    model.LogoImage = Concat(Contains.SystemSettingFolder, fileName);
                }

                if (model.LogoFooter != null && model.LogoFooter.ContentLength > 0)
                {
                    var extension = Path.GetExtension(model.LogoFooter.FileName);
                    var fileName = Concat("logoFooter", extension);
                    var path = Path.Combine(Server.MapPath(Concat("~/", Contains.SystemSettingFolder)), fileName);

                    //Check and delete logo exists
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    model.LogoFooter.SaveAs(path);
                    model.LogoFooterImage = Concat(Contains.SystemSettingFolder, fileName);
                }

                var modelMap = Mapper.Map(model, byId);
                _systemSettingService.Update(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Title, localized.Title, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.FooterContent, localized.FooterContent, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaTitle, localized.MetaTitle, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaKeywords, localized.MetaKeywords, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaDescription, localized.MetaDescription, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.TimeWork, localized.TimeWork, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Slogan, localized.Slogan, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", Format(MessageUI.UpdateSuccess, FormUI.SystemSetting)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    return RedirectToAction("Index");
                }

                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ExtentionUtils.Log(Concat("SystemSetting.Edit: ", ex.Message));

                return View(model);
            }
        }

        [RequiredPermisson(Roles = "ViewSystemSetting")]
        public ActionResult Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;

            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords,
                Sorts = new SortBuilder
                {
                    ColumnName = "CreatedDate",
                    ColumnOrder = SortBuilder.SortOrder.Descending
                }
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };
            var systemSettings = _systemSettingService.PagedList(sortingPagingBuilder, paging);
            if (systemSettings != null && systemSettings.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }

            return View(systemSettings);
        }
    }
}