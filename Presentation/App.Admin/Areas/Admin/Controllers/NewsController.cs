using App.Admin.Helpers;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Domain.Entities.Menu;
using App.FakeEntity.Menu;
using App.FakeEntity.News;
using App.Framework.Utilities;
using App.Service.Language;
using App.Service.LocalizedProperty;
using App.Service.Media;
using App.Service.Menu;
using App.Service.News;
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
    public class NewsController : BaseAdminController
    {
        private const string CacheNewsKey = "db.News";

	    private readonly IMenuLinkService _menuLinkService;

        private readonly INewsService _newsService;

        private readonly IImageService _imageService;

        private readonly ILanguageService _languageService;

        private readonly ILocalizedPropertyService _localizedPropertyService;
	    private readonly ISettingService _settingService;

		public NewsController(
            INewsService newsService
            , IMenuLinkService menuLinkService
            , IImageService imageService
            , ILanguageService languageService
            , ILocalizedPropertyService localizedPropertyService
            , ICacheManager cacheManager, ISettingService settingService)
        {
	        _newsService = newsService;
            _menuLinkService = menuLinkService;
            _imageService = imageService;
            _languageService = languageService;
            _localizedPropertyService = localizedPropertyService;
	        _settingService = settingService;

	        //Clear cache
            cacheManager.RemoveByPattern(CacheNewsKey);
        }
        
        [RequiredPermisson(Roles = "CreateEditNews")]
        public ActionResult Create()
        {
            var model = new NewsViewModel
            {
                OrderDisplay = 1,
                Status = 1
            };

            //Add locales to model
            AddLocales(_languageService, model.Locales);

            return View(model);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditNews")]
        public ActionResult Create(NewsViewModel model, string returnUrl)
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

                var titleNonAccent = model.Title.NonAccent();
                var bySeoUrl = _newsService.GetBySeoUrl(titleNonAccent);
                model.SeoUrl = model.Title.NonAccent();
                if (bySeoUrl.Any(x => x.Id != model.Id))
                {
                    var newsViewModel = model;
                    newsViewModel.SeoUrl = string.Concat(newsViewModel.SeoUrl, "-", bySeoUrl.Count());
                }

                ImageHandler(model);

	            if (model.MenuId > 0)
                {
                    var byId = _menuLinkService.GetMenu(model.MenuId, false);
                    model.VirtualCatUrl = byId.VirtualSeoUrl;
                    model.VirtualCategoryId = byId.VirtualId;
                }

                var modelMap = Mapper.Map<NewsViewModel, News>(model);
                _newsService.Create(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Title, localized.Title, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.ShortDesc, localized.ShortDesc, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.SeoUrl, localized.Title.NonAccent(), localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaTitle, localized.MetaTitle, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaKeywords, localized.MetaKeywords, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaDescription, localized.MetaDescription, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.CreateSuccess, FormUI.News)));
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
                ExtentionUtils.Log(string.Concat("News.Create: ", ex.Message));
                ModelState.AddModelError("", ex.Message);

                return View(model);
            }

            return action;
        }

	    private void ImageHandler(NewsViewModel model)
	    {
		    var folderName = Utils.FolderName(model.Title);
		    if (model.Image != null && model.Image.ContentLength > 0)
		    {
			    var fileNameOriginal = Path.GetFileNameWithoutExtension(model.Image.FileName);
			    var fileExtension = Path.GetExtension(model.Image.FileName);

			    var fileNameBg = fileNameOriginal.FileNameFormat(fileExtension);
			    var fileNameMd = fileNameOriginal.FileNameFormat(fileExtension);
			    var fileNameSm = fileNameOriginal.FileNameFormat(fileExtension);

			    var sizeWidthBg = _settingService.GetSetting("News.WidthBigSize", ImageSize.WidthDefaultSize);
			    var sizeHeightBg = _settingService.GetSetting("News.HeightBigSize", ImageSize.HeightDefaultSize);
			    var sizeWidthMd = _settingService.GetSetting("News.WidthMediumSize", ImageSize.WidthDefaultSize);
			    var sizeHeightMd = _settingService.GetSetting("News.HeightMediumSize", ImageSize.HeightDefaultSize);
			    var sizeWidthSm = _settingService.GetSetting("News.WidthSmallSize", ImageSize.WidthDefaultSize);
			    var sizeHeightSm = _settingService.GetSetting("News.HeightSmallSize", ImageSize.HeightDefaultSize);

		        //CroppedImage.SaveCroppedImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileNameBg, sizeWidthBg, sizeHeightBg);
		        //CroppedImage.SaveCroppedImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileNameMd, sizeWidthMd, sizeHeightMd);
		        //CroppedImage.SaveCroppedImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileNameSm, sizeWidthSm, sizeHeightSm);

                _imageService.CropAndResizeImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileNameBg, sizeWidthBg, sizeHeightBg);
                _imageService.CropAndResizeImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileNameMd, sizeWidthMd, sizeHeightMd);
                _imageService.CropAndResizeImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileNameSm, sizeWidthSm, sizeHeightSm);

                model.ImageBigSize = $"{Contains.NewsFolder}{folderName}/{fileNameBg}";
			    model.ImageMediumSize = $"{Contains.NewsFolder}{folderName}/{fileNameMd}";
			    model.ImageSmallSize = $"{Contains.NewsFolder}{folderName}/{fileNameSm}";
		    }
	    }

	    [RequiredPermisson(Roles = "DeleteNews")]
        public ActionResult Delete(int[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var news =
                        from id in ids
                        select _newsService.GetById(id);
                    _newsService.BatchDelete(news);

                    //Delete localize
                    for (var i = 0; i < ids.Length; i++)
                    {
                        var ieLocalizedProperty
                           = _localizedPropertyService.GetByEntityId(ids[i]);

                        _localizedPropertyService.BatchDelete(ieLocalizedProperty);
                    }
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                ExtentionUtils.Log(string.Concat("Post.Delete: ", exception.Message));
            }
            return RedirectToAction("Index");
        }

        [RequiredPermisson(Roles = "CreateEditNews")]
        public ActionResult Edit(int id)
        {
            var modelMap = Mapper.Map<News, NewsViewModel>(_newsService.GetById(id));

            //Add Locales to model
            AddLocales(_languageService, modelMap.Locales, (locale, languageId) =>
            {
                locale.Id = modelMap.Id;
                locale.LocalesId = modelMap.Id;
                locale.Title = modelMap.GetLocalized(x => x.Title, id, languageId, false, false);
                locale.ShortDesc = modelMap.GetLocalized(x => x.ShortDesc, id, languageId, false, false);
                locale.Description = modelMap.GetLocalized(x => x.Description, id, languageId, false, false);
                locale.MetaTitle = modelMap.GetLocalized(x => x.MetaTitle, id, languageId, false, false);
                locale.MetaKeywords = modelMap.GetLocalized(x => x.MetaKeywords, id, languageId, false, false);
                locale.MetaDescription = modelMap.GetLocalized(x => x.MetaDescription, id, languageId, false, false);
                locale.SeoUrl = modelMap.GetLocalized(x => x.SeoUrl, id, languageId, false, false);
            });

            return View(modelMap);
        }

        [HttpPost]
        [RequiredPermisson(Roles = "CreateEditNews")]
        public ActionResult Edit(NewsViewModel model, string returnUrl)
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

                var byId = _newsService.GetById(model.Id, false);

                var titleNonAccent = model.Title.NonAccent();
                var bySeoUrl = _menuLinkService.GetListSeoUrl(titleNonAccent, false);
                model.SeoUrl = model.Title.NonAccent();

                if (bySeoUrl.Any(x => x.Id != model.Id))
                {
                    var newsViewModel = model;
                    newsViewModel.SeoUrl = string.Concat(newsViewModel.SeoUrl, "-", bySeoUrl.Count());
                }

	            ImageHandler(model);

				//var folderName = Utils.FolderName(model.Title);
				//if (model.Image != null && model.Image.ContentLength > 0)
				//{
				//    var fileNameOriginal = Path.GetFileNameWithoutExtension(model.Image.FileName);
				//    var fileExtension = Path.GetExtension(model.Image.FileName);

				//    var fileName1 = fileNameOriginal.FileNameFormat(fileExtension);
				//    var fileName2 = fileNameOriginal.FileNameFormat(fileExtension);
				//    var fileName3 = fileNameOriginal.FileNameFormat(fileExtension);

				//    _imageService.CropAndResizeImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileName1, ImageSize.NewsWithBigSize, ImageSize.NewsHeightBigSize);
				//    _imageService.CropAndResizeImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileName2, ImageSize.NewsWithMediumSize, ImageSize.NewsHeightMediumSize);
				//    _imageService.CropAndResizeImage(model.Image, $"{Contains.NewsFolder}{folderName}/", fileName3, ImageSize.NewsWithSmallSize, ImageSize.NewsHeightSmallSize);

				//    model.ImageBigSize = $"{Contains.NewsFolder}{folderName}/{fileName1}";
				//    model.ImageMediumSize = $"{Contains.NewsFolder}{folderName}/{fileName2}";
				//    model.ImageSmallSize = $"{Contains.NewsFolder}{folderName}/{fileName3}";
				//}

				if (model.MenuId > 0)
                {
                    var menuLink = _menuLinkService.GetMenu(model.MenuId, false);
                    model.VirtualCatUrl = menuLink.VirtualSeoUrl;
                    model.VirtualCategoryId = menuLink.VirtualId;
                    model.MenuLink = Mapper.Map<MenuLink, MenuLinkViewModel>(menuLink);
                }

                var modelMap = Mapper.Map(model, byId);
                _newsService.Update(modelMap);

                //Update Localized   
                foreach (var localized in model.Locales)
                {
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Title, localized.Title, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.ShortDesc, localized.ShortDesc, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.Description, localized.Description, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.SeoUrl, localized.Title.NonAccent(), localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaTitle, localized.MetaTitle, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaKeywords, localized.MetaKeywords, localized.LanguageId);
                    _localizedPropertyService.SaveLocalizedValue(modelMap, x => x.MetaDescription, localized.MetaDescription, localized.LanguageId);
                }

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.News)));
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
                ModelState.AddModelError("", ex.Message);
                ExtentionUtils.Log(string.Concat("News.Edit: ", ex.Message));

                return View(model);
            }

            return action;
        }

        [RequiredPermisson(Roles = "ViewNews")]
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

            var news = _newsService.PagedList(sortingPagingBuilder, paging);
            if (news != null && news.Any())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }
            return View(news);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.RouteData.Values["action"].Equals("create") || filterContext.RouteData.Values["action"].Equals("edit") || filterContext.RouteData.Values["action"].ToString().ToLower().Equals("index"))
            {
                var menuLinks = _menuLinkService.FindBy(x => x.Status == 1 && x.TemplateType == 1 || x.TemplateType == 6 || x.TemplateType == 7, true);
                ViewBag.MenuList = menuLinks;
            }
        }
    }
}