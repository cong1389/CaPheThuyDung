using App.Admin.Helpers;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Shippings;
using App.FakeEntity.Shippings;
using App.Framework.Utilities;
using App.Service.Language;
using App.Service.LocalizedProperty;
using App.Service.Media;
using App.Service.Settings;
using App.Service.ShippingMethodes;
using AutoMapper;
using Resources;
using System;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class ShippingMethodController : BaseAdminController
    {
        private const string CacheShippingMethod = "db.ShippingMethod";

        private readonly IShippingMethodService _paymentMethodService;

	    private readonly ILocalizedPropertyService _localizedPropertyService;
        private readonly ILanguageService _languageService;

	    public ShippingMethodController(IShippingMethodService paymentMethodService, IImageService imageService
            , ICacheManager cacheManager
            , ILocalizedPropertyService localizedPropertyService
            , ILanguageService languageService, ISettingService settingService)
        {
            _paymentMethodService = paymentMethodService;
	        _languageService = languageService;
	        _localizedPropertyService = localizedPropertyService;

            //Clear cache
            cacheManager.RemoveByPattern(CacheShippingMethod);

        }

		[RequiredPermisson(Roles = "CreateEditShippingMethod")]
		public ActionResult Edit(int id)
		{
			var modelMap = Mapper.Map<ShippingMethod, ShippingMethodViewModel>(_paymentMethodService.Get(x => x.Id == id));

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
		[RequiredPermisson(Roles = "CreateEditShippingMethod")]
		public ActionResult Edit(ShippingMethodViewModel model, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(model);
				}

				var byId = _paymentMethodService.Get(x => x.Id == model.Id);
				
				var modelMap = Mapper.Map(model, byId);
				_paymentMethodService.Update(modelMap);

				//Update Localized   
				foreach (var localized in model.Locales)
				{
					_localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Name, localized.Name, localized.LanguageId);
					_localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
				}


				Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Name)));
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
				ExtentionUtils.Log(string.Concat("ShippingMethod.Edit: ", ex.Message));
				return View(model);
			}

			return action;
		}

		[RequiredPermisson(Roles = "ViewShippingMethod")]
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

            var manufacturers = _paymentMethodService.PagedList(sortingPagingBuilder, paging);
            if (manufacturers.IsAny())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }

            return View(manufacturers);
        }
    }
}