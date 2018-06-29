using App.Domain.Common;
using App.Front.Extensions;
using App.Front.Models;
using App.Service.Menu;
using App.Service.Static;
using System.Linq;
using System.Web.Mvc;

namespace App.Front.Controllers
{
    public class StaticContentController : FrontBaseController
    {
        private readonly IStaticContentService _staticContentService;

        public StaticContentController(IStaticContentService staticContentService
            , IMenuLinkService menuLinkService
            )
        {
            _staticContentService = staticContentService;
        }

        public ActionResult ContentDetail(int id)
        {
            var staticContent = _staticContentService.GetById(id);

            if (staticContent == null)
            {
                return HttpNotFound();
            }

            return PartialView(staticContent);
        }

        [PartialCache("Long","*")]
        [ChildActionOnly]
        public ActionResult Contents(string virtualCategoryId)
        {
            var staticContents = _staticContentService.GetStaticContents(virtualCategoryId, (int)Status.Enable);

            if (staticContents == null)
            {
                return HttpNotFound();
            }

            staticContents = staticContents.Select(x => x.ToModel());

            return PartialView(staticContents);
        }
    }
}