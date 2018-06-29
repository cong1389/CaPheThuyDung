using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Language;
using App.FakeEntity.Language;
using App.Framework.Utilities;
using App.Service.Language;
using AutoMapper;
using Resources;

namespace App.Admin.Controllers
{
    public class LanguageController : BaseAdminController
    {
        #region Language
        private const string CacheLanguageKey = "db.Language";

	    private readonly ILanguageService _langService;
        
        public LanguageController(ILanguageService langService
            , ICacheManager cacheManager)
        {
	        _langService = langService;

	        //Clear cache
            cacheManager.RemoveByPattern(CacheLanguageKey);

        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new LanguageFormViewModel
            {
                Status = 1
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LanguageFormViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var messages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                            .Select(v => v.ErrorMessage + " " + v.Exception));
                    ModelState.AddModelError("", messages);
                    return View(model);
                }

                if (model.File != null && model.File.ContentLength > 0)
                {
                    var fileNameOriginal = Path.GetFileNameWithoutExtension(model.File.FileName);
                    var fileExtension = Path.GetExtension(model.File.FileName);
                    var fileName = fileNameOriginal.FileNameFormat(fileExtension);

                    //var fileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                    var path = Path.Combine(Server.MapPath(string.Concat("~/", Contains.FolderLanguage)), fileName);

                    model.File.SaveAs(path);
                    model.Flag = string.Concat(Contains.FolderLanguage, fileName);
                }

                var modelMap = Mapper.Map<LanguageFormViewModel, Language>(model);
                _langService.CreateLanguage(modelMap);

                if (_langService.SaveLanguage() > 0)
                {
                    Response.Cookies.Add(new HttpCookie("system_message", MessageUI.SuccessLanguage));
                    if (!Url.IsLocalUrl(returnUrl) || returnUrl.Length <= 1 || !returnUrl.StartsWith("/") || returnUrl.StartsWith("//") || returnUrl.StartsWith("/\\"))
                    {
                        return RedirectToAction("Index");
                        
                    }
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", MessageUI.ErrorMessage);

                return View(model);
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Language.Create: ", ex.Message));
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var languageViewModel = Mapper.Map<Language, LanguageFormViewModel>(_langService.GetById(id));
            return View(languageViewModel);
        }

        [HttpPost]
        public ActionResult Edit(LanguageFormViewModel model, string returnUrl)
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

                if (model.File != null && model.File.ContentLength > 0)
                {
                    var fileNameOriginal = Path.GetFileNameWithoutExtension(model.File.FileName);
                    var fileExtension = Path.GetExtension(model.File.FileName);
                    var fileName = fileNameOriginal.FileNameFormat(fileExtension);

                    //var fileName = Path.GetFileNameWithoutExtension(model.File.FileName);
                
                    var path = Path.Combine(Server.MapPath(string.Concat("~/", Contains.FolderLanguage)), fileName);

                    model.File.SaveAs(path);
                    model.Flag = string.Concat(Contains.FolderLanguage, fileName);
                }

                var modelMap = Mapper.Map<LanguageFormViewModel, Language>(model);
                _langService.Update(modelMap);

                Response.Cookies.Add(new HttpCookie("system_message", string.Format(MessageUI.UpdateSuccess, FormUI.Language)));
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
                ExtentionUtils.Log(string.Concat("Language.Edit: ", ex.Message));

                return View(model);
            }

            return action;
        }

        public ActionResult Delete(string[] ids)
        {
            try
            {
                if (ids.Length != 0)
                {
                    var language =
                        from id in ids
                        select _langService.GetById(int.Parse(id));
                    _langService.BatchDelete(language);
                }
            }
            catch (Exception ex)
            {
                ExtentionUtils.Log(string.Concat("Language.Delete: ", ex.Message));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Index(int page = 1, string keywords = "")
        {
            ViewBag.Keywords = keywords;
            var sortingPagingBuilder = new SortingPagingBuilder
            {
                Keywords = keywords,
                Sorts = new SortBuilder
                {
                    ColumnName = "LanguageCode",
                    ColumnOrder = SortBuilder.SortOrder.Descending
                }
            };
            var paging = new Paging
            {
                PageNumber = page,
                PageSize = PageSize,
                TotalRecord = 0
            };

            var languages = _langService.PagedList(sortingPagingBuilder, paging);
            if (languages.IsAny())
            {
                var pageInfo = new Helper.PageInfo(ExtentionUtils.PageSize, page, paging.TotalRecord, i => Url.Action("Index", new { page = i, keywords }));
                ViewBag.PageInfo = pageInfo;
            }

            return View(languages);
        }

        #endregion

    }
}