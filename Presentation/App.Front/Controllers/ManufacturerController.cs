using System.Web.Mvc;
using App.Domain.Common;
using App.Front.Models;
using App.Service.Manufacturers;

namespace App.Front.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;
        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [PartialCache("Long")]
        public ActionResult ManufacturerHome()
        {
            var manufacturers = _manufacturerService.FindBy(x => x.Status == (int)Status.Enable);

            return PartialView(manufacturers);
        }

    }
}