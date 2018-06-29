using App.Aplication;
using App.Domain.Common;
using App.FakeEntity.Menu;
using App.Front.Extensions;
using App.Front.Models;
using App.Service.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Front.Controllers
{
    public class MenuNavController : FrontBaseController
    {
        private readonly IMenuLinkService _menuLinkService;

        public MenuNavController(IMenuLinkService menuLinkService)
        {
            _menuLinkService = menuLinkService;
        }

        [ChildActionOnly]
        public ActionResult FooterLink()
        {
            var menuNavs = new List<MenuNavViewModel>();

            var menuLinks = _menuLinkService.GetByOptions(new List<int> { (int)Position.Footer });

            if (!menuLinks.IsAny())
            {
                return PartialView(menuNavs);
            }

            var navViewModels = menuLinks.Select(x => new MenuNavViewModel
            {
                MenuId = x.Id,
                ParentId = x.ParentId,
                MenuName = x.MenuName,
                SeoUrl = x.SeoUrl,
                OrderDisplay = x.OrderDisplay,
                ImageBigSize = x.ImageBigSize,
                CurrentVirtualId = x.CurrentVirtualId,
                VirtualId = x.VirtualId,
                TemplateType = x.TemplateType,
                ImageMediumSize = x.ImageMediumSize,
                ImageSmallSize = x.ImageSmallSize
            });
            menuNavs = MenuNavExtensions.MenuNavsViewModels(null, navViewModels);

            return PartialView(menuNavs);
        }

        [PartialCache("Long")]
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var menuNavs = PrepareTopMenuNavs();

            return PartialView(menuNavs);
        }

        [ChildActionOnly]
        [PartialCache("Long")]
        public ActionResult TopHead()
        {
            var menuLinks = _menuLinkService.GetByOptions(new List<int> { (int)Position.TopHead });
            if (!menuLinks.IsAny())
            {
                return HttpNotFound();
            }

            //Convert to localized
            menuLinks = menuLinks.Select(x => x.ToModel());

            var menuNav = menuLinks.Select(x => new MenuNavViewModel
            {
                MenuId = x.Id,
                ParentId = x.ParentId,
                MenuName = x.MenuName,
                SeoUrl = x.SeoUrl,
                OrderDisplay = x.OrderDisplay,
                ImageBigSize = x.ImageBigSize,
                CurrentVirtualId = x.CurrentVirtualId,
                VirtualId = x.VirtualId,
                TemplateType = x.TemplateType,
                ImageMediumSize = x.ImageMediumSize,
                ImageSmallSize = x.ImageSmallSize
            });

            return PartialView(menuNav);
        }

        public List<MenuNavViewModel> PrepareTopMenuNavs()
        {
            var menuLinks = _menuLinkService.GetByOptions(new List<int> { (int)Position.Top });
            if (!menuLinks.IsAny())
            {
                return new List<MenuNavViewModel>();
            }
            //Convert to localized
            menuLinks = menuLinks.Select(x => x.ToModel());

            var menuNav = menuLinks.Select(x => new MenuNavViewModel
            {
                MenuId = x.Id,
                ParentId = x.ParentId,
                MenuName = x.MenuName,
                SeoUrl = x.SeoUrl,
                OtherLink = x.SourceLink,
                OrderDisplay = x.OrderDisplay,
                CurrentVirtualId = x.CurrentVirtualId,
                VirtualId = x.VirtualId,
                TemplateType = x.TemplateType,
                ImageBigSize = x.ImageBigSize,
                ImageMediumSize = x.ImageMediumSize,
                ImageSmallSize = x.ImageSmallSize
            });

            return MenuNavExtensions.MenuNavsViewModels(null, menuNav);
        }

        [PartialCache("Long","*")]
        [ChildActionOnly]
        public PartialViewResult MenuNavLeft(string virtualId)
        {
            var ieMenuNav = PrepareMenuNavBase(virtualId);

            return PartialView(ieMenuNav);
        }

        [PartialCache("Long", "*")]
        [ChildActionOnly]
        public PartialViewResult MenuNavNews(string virtualId)
        {
            var navViewModels = PrepareMenuNavBase(virtualId);

            return PartialView(navViewModels);
        }

        [PartialCache("Long", "*")]
        [ChildActionOnly]
        public PartialViewResult MenuHomeSlide(string virtualId)
        {
            var ieMenuNav = PrepareMenuNavBase(virtualId);

            return PartialView(ieMenuNav);
        }

        [PartialCache("Long", "*")]
        [NonAction]
        private IEnumerable<MenuNavViewModel> PrepareMenuNavBase(string virtualId)
        {
            virtualId = virtualId != null && virtualId.Count(i => i.Equals('/')) > 0
                ? virtualId.Split('/')[0]
                : virtualId;

            var menuLinks = _menuLinkService.GetByOptions(virtualId: virtualId);

            if (!menuLinks.IsAny())
            {
                return null;
            }

            //Convert to localized
            menuLinks = menuLinks.Where(x => x.ParentId != null).Select(x => x.ToModel()).OrderBy(x => x.OrderDisplay);

            var navViewModels = menuLinks.Select(x => new MenuNavViewModel
            {
                MenuId = x.Id,
                ParentId = x.ParentId,
                MenuName = x.MenuName,
                SeoUrl = x.SeoUrl,
                OrderDisplay = x.OrderDisplay,
                ImageBigSize = x.ImageBigSize,
                CurrentVirtualId = x.CurrentVirtualId,
                VirtualId = x.VirtualId,
                TemplateType = x.TemplateType,
                ImageMediumSize = x.ImageMediumSize,
                ImageSmallSize = x.ImageSmallSize
            });
            return navViewModels;

        }

        [PartialCache("Long", "*")]
        [ChildActionOnly]
        public ActionResult MenuVerticalMega()
        {
            var menuLinks = _menuLinkService.GetByOptions(new List<int> { (int)Position.SiderBar });
            if (!menuLinks.IsAny())
            {
                return HttpNotFound();
            }

            var navViewModels =
                from x in menuLinks
                select new MenuNavViewModel
                {
                    MenuId = x.Id,
                    ParentId = x.ParentId,
                    MenuName = x.MenuName,
                    SeoUrl = x.SeoUrl,
                    OrderDisplay = x.OrderDisplay,
                    ImageBigSize = x.ImageBigSize,
                    CurrentVirtualId = x.CurrentVirtualId,
                    VirtualId = x.VirtualId,
                    TemplateType = x.TemplateType,
                    ImageMediumSize = x.ImageMediumSize,
                    ImageSmallSize = x.ImageSmallSize
                };

            var menuNavs = MenuNavExtensions.MenuNavsViewModels(null, navViewModels);
            //var menuNavs = CreateMenuNav(null, menuNav);

            return PartialView(menuNavs);
        }

        [PartialCache("Long", "*")]
        [ChildActionOnly]
        public ActionResult MenuLinkSideBar(string virtualId, List<int> proAttrs = null)
        {
            virtualId = virtualId != null && virtualId.Count(i => i.Equals('/')) > 0
                ? virtualId.Split('/')[0]
                : virtualId;

            var menuLinks = _menuLinkService.GetByOptions(virtualId: virtualId);

            if (!menuLinks.IsAny())
            {
                return HttpNotFound();
            }

            //Convert to localized
            menuLinks = menuLinks.Select(x => x.ToModel());

            var navViewModels =
                from x in menuLinks
                select new MenuNavViewModel
                {
                    MenuId = x.Id,
                    ParentId = x.ParentId,
                    MenuName = x.MenuName,
                    SeoUrl = x.SeoUrl,
                    OrderDisplay = x.OrderDisplay,
                    ImageBigSize = x.ImageBigSize,
                    CurrentVirtualId = x.CurrentVirtualId,
                    VirtualId = x.VirtualId,
                    TemplateType = x.TemplateType,
                    ImageMediumSize = x.ImageMediumSize,
                    ImageSmallSize = x.ImageSmallSize
                };


            var menuNavs = MenuNavExtensions.MenuNavsViewModels(null, navViewModels);
            //var menuNavs = CreateMenuNav(null, menuNav);

            ViewBag.ProAttrs = proAttrs;

            return PartialView(menuNavs);
        }

        [PartialCache("Long", "*")]
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [PartialCache("Long", "*")]
        public ActionResult Services(string virtualId)
        {
            var menuLinks = PrepareMenuNavBase(virtualId);

            return PartialView(menuLinks);
        }
    }


}