using App.Admin.Helpers;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Payments;
using App.FakeEntity.Payments;
using App.Framework.Utilities;
using App.Service.Language;
using App.Service.LocalizedProperty;
using App.Service.Media;
using App.Service.PaymentMethodes;
using App.Service.Settings;
using AutoMapper;
using Resources;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Admin.Controllers
{
    public class PaymentMethodController : BaseAdminController
	{
		private const string CachePaymentMethod = "db.PaymentMethod";

		private readonly IPaymentMethodService _paymentMethodService;

		private readonly IImageService _imageService;

		private readonly ILocalizedPropertyService _localizedPropertyService;
		private readonly ILanguageService _languageService;
		private readonly ISettingService _settingService;

		public PaymentMethodController(IPaymentMethodService paymentMethodService, IImageService imageService
			, ICacheManager cacheManager
			, ILocalizedPropertyService localizedPropertyService
			, ILanguageService languageService, ISettingService settingService)
		{
			_paymentMethodService = paymentMethodService;
			_imageService = imageService;
			_languageService = languageService;
			_settingService = settingService;
			_localizedPropertyService = localizedPropertyService;

			//Clear cache
			cacheManager.RemoveByPattern(CachePaymentMethod);

		}

		[RequiredPermisson(Roles = "CreateEditPaymentMethod")]
		public ActionResult Create()
		{
			var model = new PaymentMethodViewModel
			{
				Status = 1,
				OrderDisplay = 1
			};

			//Add locales to model
			AddLocales(_languageService, model.Locales);

			return View(model);
		}

		[HttpPost]
		[RequiredPermisson(Roles = "CreateEditPaymentMethod")]
		public ActionResult Create(PaymentMethodViewModel model, string returnUrl)
		{
			ActionResult action;
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("", MessageUI.ErrorMessage);
					return View(model);
				}

				ImageHandler(model);

				var modelMap = Mapper.Map<PaymentMethodViewModel, PaymentMethod>(model);
				_paymentMethodService.Create(modelMap);

				//Update Localized   
				foreach (var localized in model.Locales)
				{
					_localizedPropertyService.SaveLocalizedValue(modelMap, x => x.PaymentMethodSystemName, localized.PaymentMethodSystemName, localized.LanguageId);
					_localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
				}

				Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.PaymentMethod)));
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
				ExtentionUtils.Log(string.Concat("PaymentMethod.Create: ", ex.Message));
				ModelState.AddModelError("", ex.Message);

				return View(model);
			}

			return action;
		}

		private void ImageHandler(PaymentMethodViewModel model)
		{
			if (model.Image != null && model.Image.ContentLength > 0)
			{
				var folderName = Utils.FolderName(model.PaymentMethodSystemName);
				var fileExtension = Path.GetExtension(model.Image.FileName);
				var fileNameOriginal = Path.GetFileNameWithoutExtension(model.Image.FileName);

				var fileName = fileNameOriginal.FileNameFormat(fileExtension);

				var sizeWidthBg = _settingService.GetSetting("PaymentMethod.WidthBigSize", ImageSize.WidthDefaultSize);
				var sizeHeighthBg = _settingService.GetSetting("PaymentMethod.HeightBigSize", ImageSize.HeightDefaultSize);

				_imageService.CropAndResizeImage(model.Image, $"{Contains.PaymentMethodFolder}{folderName}/", fileName, sizeWidthBg, sizeHeighthBg);

				model.ImageUrl = $"{Contains.PaymentMethodFolder}{folderName}/{fileName}";// string.Concat(Contains.PaymentMethodFolder, fileName);
			}
		}

		[RequiredPermisson(Roles = "CreateEditPaymentMethod")]
		public ActionResult Delete(int[] ids)
		{
			try
			{
				if (ids.Length != 0)
				{
					var paymentMethod =
						from id in ids
						select _paymentMethodService.Get(x => x.Id == id);
					_paymentMethodService.BatchDelete(paymentMethod);
				}
			}
			catch (Exception ex)
			{
				ExtentionUtils.Log(string.Concat("PaymentMethod.Delete: ", ex.Message));
			}

			return RedirectToAction("Index");
		}

		[RequiredPermisson(Roles = "CreateEditPaymentMethod")]
		public ActionResult Edit(int id)
		{
			var modelMap = Mapper.Map<PaymentMethod, PaymentMethodViewModel>(_paymentMethodService.Get(x => x.Id == id));

			//Add Locales to model
			AddLocales(_languageService, modelMap.Locales, (locale, languageId) =>
			{
				locale.Id = modelMap.Id;
				locale.LocalesId = modelMap.Id;
				locale.PaymentMethodSystemName = modelMap.GetLocalized(x => x.PaymentMethodSystemName, id, languageId, false, false);
				locale.Description = modelMap.GetLocalized(x => x.Description, id, languageId, false, false);
			});

			return View(modelMap);
		}

		[HttpPost]
		[RequiredPermisson(Roles = "CreateEditPaymentMethod")]
		public ActionResult Edit(PaymentMethodViewModel model, string returnUrl)
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

				ImageHandler(model);

				//if (model.Image != null && model.Image.ContentLength > 0)
				//{
				//    var fileExtension = Path.GetExtension(model.Image.FileName);
				//    var fileNameOriginal = Path.GetFileNameWithoutExtension(model.Image.FileName);

				//    var fileName = fileNameOriginal.FileNameFormat(fileExtension);

				//    //var fileExtension = Path.GetExtension(model.Image.FileName);
				//    //var fileName = titleNonAccent.FileNameFormat(fileExtension);

				//    _imageService.CropAndResizeImage(model.Image, $"{Contains.PaymentMethodFolder}", fileName, ImageSize.PaymentMethodWithMediumSize, ImageSize.PaymentMethodHeightMediumSize);

				//    model.ImageUrl = string.Concat(Contains.PaymentMethodFolder, fileName);
				//}

				var modelMap = Mapper.Map(model, byId);
				_paymentMethodService.Update(modelMap);

				//Update Localized   
				foreach (var localized in model.Locales)
				{
					_localizedPropertyService.SaveLocalizedValue(modelMap, x => x.PaymentMethodSystemName, localized.PaymentMethodSystemName, localized.LanguageId);
					_localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
				}


				Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.PaymentMethod)));
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
				ExtentionUtils.Log(string.Concat("PaymentMethod.Edit: ", ex.Message));
				return View(model);
			}

			return action;
		}

		[RequiredPermisson(Roles = "ViewPaymentMethod")]
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