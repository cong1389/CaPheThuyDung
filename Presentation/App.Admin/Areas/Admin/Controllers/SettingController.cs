using App.Core.Caching;
using App.Domain.Entities.Setting;
using App.FakeEntity.Setting;
using App.Service.Settings;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using App.Aplication.Extensions;

namespace App.Admin.Controllers
{
	public class SettingController : BaseAdminController
	{
		private const string Cache = "db.Setting";

		private readonly ISettingService _settingService;

		public SettingController(
			ISettingService settingService, ICacheManager cacheManager)
		{
			_settingService = settingService;

			//Clear cache
			cacheManager.RemoveByPattern(Cache);
		}

		public ActionResult Index(int page = 1, string keywords = "")
		{
			var settings = _settingService.GetAll();
			keywords = keywords.ToLower().Trim();
			settings = settings.Where(x => x.Name.ToLower().Contains(keywords) || x.Value.Contains(keywords));

			ViewBag.keywords = keywords;
			ViewBag.Settings = settings.OrderBy(x => x.Name);

			return View(settings);
		}

		[HttpPost]
		public ActionResult CreateOrSave(SettingViewModel model)
		{
			var setting = _settingService.GetSetting(model.Id);

			if (setting != null)
			{
				if (!setting.Name.Equals(model.Name) && CheckNameIsExsist(model.Name))
				{
					return Json(
						new
						{
							success = false,
							message = $"{model.Name} đã tồn tại. Vui lòng chọn tên khác"
						}
						, JsonRequestBehavior.AllowGet);
				}

				var localeByMap = Mapper.Map(model, setting);
				_settingService.Update(localeByMap);
			}
			else
			{
				if (CheckNameIsExsist(model.Name))
				{
					return Json(
						new
						{
							success = false,
							message = $"{model.Name} đã tồn tại. Vui lòng chọn tên khác"
						}
						, JsonRequestBehavior.AllowGet);
				}

				var localeByMap = Mapper.Map<SettingViewModel, Setting>(model);
				_settingService.Create(localeByMap);
			}

			return Json(
				new
				{
					success = true
				}
				, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Delete(int id)
		{
			var locale = _settingService.GetSetting(id);
			_settingService.Delete(locale);

			return Json(
				new { success = true }
				, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult NewRow()
		{
			var model = new Setting();

			var newRow = this.RenderRazorViewToString("_NewRow", model);

			return Json(new { data = newRow, success = true }, JsonRequestBehavior.AllowGet);
		}

		private bool CheckNameIsExsist(string name)
		{
			var setting = _settingService.GetSetting(name: name);

			return setting != null;

		}
	}
}