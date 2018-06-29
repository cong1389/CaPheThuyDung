using App.Admin.Helpers;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.GenericControl;
using App.Domain.Entities.Menu;
using App.FakeEntity.GenericControl;
using App.FakeEntity.Menu;
using App.Framework.Utilities;
using App.Service.GenericControl;
using App.Service.Language;
using App.Service.LocalizedProperty;
using App.Service.Menu;
using AutoMapper;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
	public class GenericControlController : BaseAdminController
    {
        private const string CacheGenericcontrolKey = "db.GenericControl";

	    private readonly IGenericControlService _gCService;

        private readonly ILanguageService _languageService;

        private readonly ILocalizedPropertyService _localizedPropertyService;

        private readonly IMenuLinkService _menuLinkService;

        public GenericControlController(IGenericControlService gCService
              , ILanguageService languageService
             , ILocalizedPropertyService localizedPropertyService
            , IGenericControlValueService genericControlValueService
            , IMenuLinkService menuLinkService
            , ICacheManager cacheManager)
        {
	        _gCService = gCService;
            _languageService = languageService;
            _localizedPropertyService = localizedPropertyService;
            _menuLinkService = menuLinkService;

	        //Clear cache
            cacheManager.RemoveByPattern(CacheGenericcontrolKey);
        }

        [RequiredPermisson(Roles = "CreateEditGenericControl")]
        public ActionResult Create()
        {
            var model = new GenericControlViewModel
            {
                OrderDisplay = 1,
                Status = 1
            };

            //Add locales to model
            AddLocales(_languageService, model.Locales);

            return View(model);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditGenericControl")]
        public ActionResult Create(GenericControlViewModel model, string returnUrl)
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

                var modelMap = Mapper.Map<GenericControlViewModel, GenericControl>(model);
                _gCService.Create(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Name, localized.Name, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.GenericControl)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("GenericControl.Create: ", ex.Message));

                return View(model);
            }

            return action;
        }

        [RequiredPermisson(Roles = "DeleteGenericControl")]
        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.IsAny())
                {
                    var genericControls =
                        from id in ids
                        select _gCService.GetById(int.Parse(id));

                    _gCService.BatchDelete(genericControls);

                    //Delete localize
                    for (var i = 0; i < ids.Length; i++)
                    {
                        var localizedProperties
                           = _localizedPropertyService.GetByEntityId(int.Parse(ids[i]));

                        _localizedPropertyService.BatchDelete(localizedProperties);
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("ServerGenericControl.Delete: ", ex.Message));
            }

            return RedirectToAction("Index");
        }

        [RequiredPermisson(Roles = "CreateEditGenericControl")]
        public ActionResult Edit(int id)
        {
            var modelMap = Mapper.Map<GenericControl, GenericControlViewModel>(_gCService.GetById(id));
            
            //Add Locales to model
            AddLocales(_languageService, modelMap.Locales, (locale, languageId) =>
            {
                locale.Id = modelMap.Id;
                locale.LocalesId = modelMap.Id;
                locale.Name = modelMap.GetLocalized(x => x.Name, id, languageId, false, false);
                locale.Description = modelMap.GetLocalized(x => x.Description, id, languageId, false, false);
            });

            return View(modelMap);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditGenericControl")]
        public ActionResult Edit(GenericControlViewModel model, string returnUrl)
        {
            ActionResult action;
            try
            {
                if (!ModelState.IsValid)
                {
                    var messages = string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                         .Select(v => v.ErrorMessage + " " + v.Exception));
                    ModelState.AddModelError("", messages);

                    return View(model);
                }

                var objGenericControl = _gCService.GetById(model.Id);

                var menuLinks = new List<MenuLink>();
                var menuIds = new List<int>();

                var menuLink = Request["MenuLink"];
                if (!string.IsNullOrEmpty(menuLink))
                {
                    foreach (var item in menuLink.Split(',').ToList())
                    {
                        var menuId = int.Parse(item);
                        menuIds.Add(menuId);

                        menuLinks.Add(_menuLinkService.GetMenu(menuId,false));
                    }

                    if (menuIds.IsAny())
                    {
                        (from x in objGenericControl.MenuLinks
                                where !menuIds.Contains(x.Id)
                                select x).ToList()
                            .ForEach(m =>
                                objGenericControl.MenuLinks.Remove(m)
                            );
                    }
                }
                objGenericControl.MenuLinks = menuLinks;
                    
                var modelMap = Mapper.Map<GenericControlViewModel, GenericControl>(model);
                _gCService.Update(objGenericControl);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Name, localized.Name, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.GenericControl)));
                if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                {
                    action = RedirectToAction("Index");
                }
                else
                {
                    action = Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("GenericControl.Create: ", ex.Message));

                return View(model);
            }

            return action;
        }

        [RequiredPermisson(Roles = "ViewGenericControl")]
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

            var genericControls = _gCService.PagedList(sortingPagingBuilder, paging);

            if (genericControls != null && genericControls.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }

            return View(genericControls);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.RouteData.Values["action"].Equals("create"))
            {
                var menuLinks = _menuLinkService.GetAll();

                var model = from x in menuLinks
                    select new MenuLinkViewModel
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Status = x.Status,
                        TypeMenu = x.TypeMenu,
                        Position = x.Position,
                        MenuName = x.MenuName,
                        VirtualId = x.VirtualId
                    };

                ViewBag.GCMenuLink = model;
            }

            if (filterContext.RouteData.Values["action"].Equals("edit"))
            {
                var id = int.Parse(filterContext.RouteData.Values["id"].ToString());

                var menuLinks = _menuLinkService.GetAll();

                var model = from x in menuLinks
                                                       select new MenuLinkViewModel
                                                       {
                                                           Id = x.Id,
                                                           ParentId = x.ParentId,
                                                           Status = x.Status,
                                                           TypeMenu = x.TypeMenu,
                                                           Position = x.Position,
                                                           MenuName = x.MenuName,
                                                           VirtualId = x.VirtualId,
                                                           Selected = x.SelectedMenu(id)
                                                       };

                ViewBag.GCMenuLink = model;
            }
        }
    }
}