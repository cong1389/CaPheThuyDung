using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using App.Aplication;
using App.Front.Models;
using App.Service.ContactInformation;
using App.Service.Locations;
using Newtonsoft.Json;

namespace App.Front.Controllers
{
    public class StoreListController : FrontBaseController
	{
		private readonly IProvinceService _provinceService;

		private readonly IContactInfoService _contactInfoService;

		public StoreListController(IProvinceService provinceService, IContactInfoService contactInfoService)
		{
			_provinceService = provinceService;
			_contactInfoService = contactInfoService;
		}

		public ActionResult GetStoreListByProvince(int id)
		{
			if (!Request.IsAjaxRequest())
			{
				return Json(new { success = false });
			}
			var contactInformations = _contactInfoService.FindBy(x => x.Status == 1 && x.ProvinceId == (int?)id, true);
			var storeLists = new List<StoreList>();
			if (contactInformations.IsAny())
			{
				storeLists.AddRange(
					from item in contactInformations
					select new StoreList
					{
						Address = item.Address,
						Lat = item.Lat,
						Lng = item.Lag,
						Phone = item.MobilePhone,
						Title = item.Title
					});
			}
			return Json(new { data = storeLists, success = true });
		}

		public ActionResult Index(int id)
		{
			var province = _provinceService.GetTop(1, x => x.Status == 1, x => x.OrderDisplay).FirstOrDefault();
			var top = _provinceService.GetTop(2147483647, x => x.Status == 1, x => x.OrderDisplay);
			ViewBag.Provinces = top;
			var contactInformations = _contactInfoService.FindBy(x => x.Status == 1 && x.ProvinceId == (int?)province.Id, true);
			var storeLists = new List<StoreList>();
			if (contactInformations.IsAny())
			{
				storeLists.AddRange(
					from item in contactInformations
					select new StoreList
					{
						Address = item.Address,
						Lat = item.Lat,
						Lng = item.Lag,
						Phone = item.MobilePhone,
						Title = item.Title,
						NumberOfStore = item.NumberOfStore
					});
				ViewBag.Data = JsonConvert.SerializeObject(storeLists);
			}

			return PartialView(contactInformations);
		}
	}
}