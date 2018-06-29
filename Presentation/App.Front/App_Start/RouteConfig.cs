using System.Web.Mvc;
using System.Web.Routing;

namespace App.Front
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");
            routes.IgnoreRoute("images/{*pathInfo}");
            routes.IgnoreRoute("scripts/{*pathInfo}");
            routes.IgnoreRoute("fonts/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapRoute(null, "sitemap.xml", new { controller = "SiteMap", action = "SiteMapXml" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "getprice.html", new { controller = "Post", action = "PostPrice" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "sitemap-images.xml", new { controller = "SiteMap", action = "SiteMapImage" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "gallery-images.html", new { controller = "Post", action = "GetGallery" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "left-fixitem.html", new { controller = "Menu", action = "GetLeftFixItem" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "fixitem-content.html", new { controller = "Post", action = "PostHome" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/quan-ly-tin-rao.html", new { controller = "Account", action = "PostManagement", page = 1 }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "danh-sach-cua-hang.html", new { controller = "StoreList", action = "GetStoreListByProvince" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "ban-do/{Id}.html", new { controller = "GoogleMap", action = "ShowGoogleMap", Id = UrlParameter.Optional }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "gui-lien-he.html", new { controller = "Home", action = "SendContact" }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "thanh-vien/dang-ky.html", new { controller = "User", action = "Registration" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/dang-tin-rao.html", new { controller = "Account", action = "CreatePost" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/thay-doi-thong-tin-ca-nhan.html", new { controller = "Account", action = "ChangeInfo" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/sua-tin-rao/{Id}.html", new { controller = "Account", action = "EditPost", Id = 1 }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "filter.html", new { controller = "Post", action = "FillterProduct" }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "thanh-vien/quan-ly-tin-rao/trang-{page}.html", new { controller = "Account", action = "PostManagement", page = UrlParameter.Optional }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/tim-tin-rao.html", new { controller = "Account", action = "SearchPost" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/thoat.html", new { controller = "Account", action = "LogOff" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/doi-mat-khau.html", new { controller = "User", action = "ChangePassword" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "thanh-vien/xoa-anh.html", new { controller = "User", action = "DeleteGallery" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "callmeback.html", new { controller = "Home", action = "SendSMS" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "404.html", new { controller = "Home", action = "Error" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "dang-nhap.html", new { controller = "User", action = "Login" }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "tin-cho-ban.html", new { controller = "Post", action = "PostForYou" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "tin-moi-nhat.html", new { controller = "Post", action = "PostLatest" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "tin-theo-chu-de.html", new { controller = "News", action = "GetContentTabsNewsHome" }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "quan-huyen", new { controller = "Summary", action = "GetDistrictByProvinceId" }, new[] { "App.Front.Controllers" });
            routes.MapRoute("tim-kiem", "tim-kiem", new { controller = "Menu", action = "Search" }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "under-construction.html", new { controller = "Home", action = "UnderConstruction" }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "cart", new { controller = "ShoppingCart", action = "Cart" }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "{seoUrl}-cttps.html", new { controller = "Post", action = "PostDetail", seoUrl = UrlParameter.Optional }, new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "{seoUrl}-cttnws.html", new { controller = "News", action = "NewsDetail", seoUrl = UrlParameter.Optional }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "tin-theo-chu-de.html", new { controller = "News", action = "GetContentTabsNewsHome" }, new[] { "App.Front.Controllers" });
           
            routes.MapRoute(null, "{menu}.html", new { controller = "Menu", action = "GetContent", menu = UrlParameter.Optional, page = 1 }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "{menu}/trang-{page}.html", new { controller = "Menu", action = "GetContent", menu = UrlParameter.Optional, page = UrlParameter.Optional }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "{catUrl}/{parameters}.html", new { controller = "Post", action = "SearchResult", catUrl = UrlParameter.Optional, parameters = UrlParameter.Optional, page = 1 }, new[] { "App.Front.Controllers" });
            routes.MapRoute(null, "{catUrl}/{parameters}/trang-{page}.html"
                , new
                {
                    controller = "Post",
                    action = "SearchResult",
                    catUrl = UrlParameter.Optional,
                    parameters = UrlParameter.Optional,
                    page = UrlParameter.Optional
                }
                , new[] { "App.Front.Controllers" });

            routes.MapRoute(null, "changelanguage/{langid}",
                new { controller = "Common", action = "SetLanguage", langId = UrlParameter.Optional },
                new[] { "App.Front.Controllers" });

            routes.MapRoute("Default", "{controller}/{action}/{id}"
              , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
              , new[] { "App.Front.Controllers" });

           

            //routes.MapRoute(null, "{id}-cttstct.html",
            //  new { controller = "StaticContent", action = "ContentDetail", id = UrlParameter.Optional },
            //  new[] { "App.Front.Controllers" });

        }
        
    }
}