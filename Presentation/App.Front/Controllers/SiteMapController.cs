using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using App.Aplication;
using App.SeoSitemap;
using App.SeoSitemap.Enum;
using App.SeoSitemap.Images;
using App.Service.Menu;
using App.Service.News;
using App.Service.Post;

namespace App.Front.Controllers
{
    public class SiteMapController : Controller
	{
		private readonly IMenuLinkService _menuLinkService;

		private readonly IPostService _postService;

		private readonly INewsService _newsService;

		private readonly ISitemapProvider _sitemapProvider;

		public SiteMapController(IMenuLinkService menuLinkService, IPostService postService, INewsService newsService,
		    ISitemapProvider sitemapProvider)
		{
			_menuLinkService = menuLinkService;
			_postService = postService;
			_newsService = newsService;
			_sitemapProvider = sitemapProvider;
		}

		public ActionResult Index()
		{
			var sitemapNodes = new List<SitemapNode>();
			var menuLinks = _menuLinkService.FindBy(x => x.Status == 1, true);
			if (menuLinks.IsAny())
			{
				foreach (var menuLink in menuLinks)
				{
					sitemapNodes.Add(new SitemapNode("Normal")
					{
						Url = Url.Action("GetContent", "Menu", new { menu = menuLink.SeoUrl }, Request.Url.Scheme),
						ChangeFrequency = ChangeFrequency.Daily,
						Priority = new decimal(8, 0, 0, false, 1),
						LastModificationDate = (menuLink.UpdatedDate.HasValue ? menuLink.UpdatedDate.Value.ToString("yyyy-MM-dd") : string.Empty)
					});
				}
			}

			return _sitemapProvider.CreateSitemap(new SitemapModel(sitemapNodes));
		}

		public ActionResult SiteMapImage()
		{
			var sitemapImages = new List<SitemapImage>();
			var item = ConfigurationManager.AppSettings["SiteName"];
			var posts = 
				from x in _postService.FindBy(x => x.Status == 1, true)
				orderby x.CreatedDate descending
				select x;

			if (posts.IsAny())
			{
				foreach (var post in posts)
				{
					sitemapImages.Add(new SitemapImage(string.Concat(item, post.ImageMediumSize))
					{
						Caption = post.Title,
						Title = post.Title
					});
					if (!post.GalleryImages.IsAny())
					{
						continue;
					}
					foreach (var galleryImage in post.GalleryImages)
					{
						sitemapImages.Add(new SitemapImage(string.Concat(item, galleryImage.ImageBig))
						{
							Caption = post.Title,
							Title = post.Title
						});
					}
				}
			}
			var news = 
				from x in _newsService.FindBy(x => x.Status == 1, true)
				orderby x.CreatedDate descending
				select x;
			if (news.IsAny())
			{
				foreach (var news1 in news)
				{
					if (string.IsNullOrEmpty(news1.ImageSmallSize))
					{
						continue;
					}
					sitemapImages.Add(new SitemapImage(string.Concat(item, news1.ImageSmallSize))
					{
						Caption = news1.Title,
						Title = news1.Title
					});
				}
			}
			return _sitemapProvider.CreateSitemap(new SitemapModel(new List<SitemapNode>
			{
				new SitemapNode(string.Empty)
				{
					Images = sitemapImages
				}
			}));
		}

		public ActionResult SiteMapXml()
		{
			DateTime value;
			string str;
			string empty;
			string str1;
			var sitemapNodes = new List<SitemapNode>();
			var item = ConfigurationManager.AppSettings["SiteName"];
			sitemapNodes.Add(new SitemapNode(string.Empty)
			{
				Url = item,
				ChangeFrequency = ChangeFrequency.Always,
				Priority = 1
			});
			var menuLinks = _menuLinkService.FindBy(x => x.Status == 1, true);
			if (menuLinks.IsAny())
			{
				foreach (var menuLink in menuLinks)
				{
					var sitemapNode = new SitemapNode(string.Empty)
					{
						Url = Url.Action("GetContent", "Menu", new { menu = menuLink.SeoUrl }, Request.Url.Scheme),
						ChangeFrequency = ChangeFrequency.Daily,
						Priority = new decimal(8, 0, 0, false, 1)
					};
					if (menuLink.UpdatedDate.HasValue)
					{
						value = menuLink.UpdatedDate.Value;
						str1 = value.ToString("yyyy-MM-dd");
					}
					else
					{
						str1 = string.Empty;
					}
					sitemapNode.LastModificationDate = str1;
					sitemapNodes.Add(sitemapNode);
				}
			}
			var posts = 
				from x in _postService.FindBy(x => x.Status == 1, true)
				orderby x.CreatedDate descending
				select x;
			if (posts.IsAny())
			{
				foreach (var post in posts)
				{
					var sitemapNode1 = new SitemapNode(string.Empty)
					{
						Url = Url.Action("PostDetail", "Post", new { seoUrl = post.SeoUrl }, Request.Url.Scheme),
						ChangeFrequency = ChangeFrequency.Daily,
						Priority = new decimal(5, 0, 0, false, 1)
					};
					if (post.UpdatedDate.HasValue)
					{
						value = post.UpdatedDate.Value;
						empty = value.ToString("yyyy-MM-dd");
					}
					else
					{
						empty = string.Empty;
					}
					sitemapNode1.LastModificationDate = empty;
					sitemapNodes.Add(sitemapNode1);
				}
			}
			var news = 
				from x in _newsService.FindBy(x => x.Status == 1, true)
				orderby x.CreatedDate descending
				select x;
			if (news.IsAny())
			{
				foreach (var news1 in news)
				{
					var sitemapNode2 = new SitemapNode(string.Empty)
					{
						Url = Url.Action("NewsDetail", "News", new { seoUrl = news1.SeoUrl }, Request.Url.Scheme),
						ChangeFrequency = ChangeFrequency.Daily,
						Priority = new decimal(5, 0, 0, false, 1)
					};
					if (news1.UpdatedDate.HasValue)
					{
						value = news1.UpdatedDate.Value;
						str = value.ToString("yyyy-MM-dd");
					}
					else
					{
						str = string.Empty;
					}
					sitemapNode2.LastModificationDate = str;
					sitemapNodes.Add(sitemapNode2);
				}
			}

			return _sitemapProvider.CreateSitemap(new SitemapModel(sitemapNodes));
		}
	}
}