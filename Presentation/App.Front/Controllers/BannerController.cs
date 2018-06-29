using System.Collections.Generic;
using System.Web.Mvc;
using App.Domain.Common;
using App.Service.Ads;

namespace App.Front.Controllers
{
    public class BannerController : FrontBaseController
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [ChildActionOnly]
        public ActionResult BannerProductHome()
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.HomeProduct });

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerTop(int? menuId, string title)
        {
            var banners = _bannerService.GetBanners(menuId, (int)Status.Enable, new List<int> { (int)Position.Top });
            //var banners = _bannerService.FindBy(x =>
            //    x.MenuId == menuId && x.Status == 1 && x.PageBanner.Position == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            ViewBag.Title = title;

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerBottom()
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.Bottom });

            //var banners = _bannerService.FindBy(x =>
            //    x.PageBanner.Position == 9 && x.Status == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerFooter()
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.Footer });

            //var banners = _bannerService.FindBy(x =>
            //    x.PageBanner.Position == 2 && x.Status == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerLeft()
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.Left });

            //var banners = _bannerService.FindBy(x =>
            //    x.PageBanner.Position == 3 && x.Status == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerMiddle(int? menuId)
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.Middle });

            //var banners = _bannerService.FindBy(x =>
            //    x.MenuId == menuId && x.PageBanner.Position == 6 && x.Status == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerOnMenu()
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.OnMenu });

            //var banners = _bannerService.FindBy(x =>
            //    x.PageBanner.Position == 8 && x.Status == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerRight()
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.Right });

            //var banners = _bannerService.FindBy(x =>
            //    x.PageBanner.Position == 4 && x.Status == 1 &&
            //    (!x.FromDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
            //    (!x.ToDate.HasValue ||
            //     DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            return PartialView(banners);
        }

        [ChildActionOnly]
        public ActionResult BannerSideBar(int? menuId)
        {
            var banners = _bannerService.GetBanners(status: (int)Status.Enable, position: new List<int> { (int)Position.SiderBar });

            return PartialView(banners);
        }
    }
}