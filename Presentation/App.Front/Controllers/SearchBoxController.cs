using System.Collections.Generic;
using System.Web.Mvc;
using App.Domain.Common;
using App.Front.Models;
using App.Service.Attribute;

namespace App.Front.Controllers
{
    public class SearchBoxController : Controller
    {
        private readonly IAttributeService _attributeService;

        public SearchBoxController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        public ActionResult SearchMenu()
        {
            var seachConditions = new SeachConditions();

            return PartialView(seachConditions);
        }

        public ActionResult SearchAttribute(List<int> attributes = null)
        {
            ViewBag.Attributes = attributes;

            var attrs = _attributeService.FindBy(x => x.Status == (int)Status.Enable);

            return PartialView(attrs);
        }

        public ActionResult SearchSideBar(int? productOld, int? productNew, List<int> attributes = null,
            List<decimal> prices = null, List<int> proAttrs = null, string virtualId = null)
        {
            ViewBag.Attributes = attributes;
            ViewBag.ProAttrs = proAttrs;
            ViewBag.Prices = prices;
            ViewBag.ProductOld = productOld;
            ViewBag.ProductNew = productNew;
            ViewBag.VirtualId = virtualId;

            return PartialView();
        }
    }
}