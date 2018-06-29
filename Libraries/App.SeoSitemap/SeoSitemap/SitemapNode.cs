using System.Collections.Generic;
using System.Xml.Serialization;
using App.SeoSitemap.Common;
using App.SeoSitemap.Enum;
using App.SeoSitemap.Images;
using App.SeoSitemap.Mobile;
using App.SeoSitemap.News;
using App.SeoSitemap.Translations;
using App.SeoSitemap.Videos;

namespace App.SeoSitemap
{
    [XmlRoot("url", Namespace="http://www.sitemaps.org/schemas/sitemap/0.9")]
	public class SitemapNode
	{
		[XmlElement("changefreq", Order=3)]
		public ChangeFrequency? ChangeFrequency
		{
			get;
			set;
		}

		[XmlElement("image", Order=5, Namespace="http://www.google.com/schemas/sitemap-image/1.1")]
		public List<SitemapImage> Images
		{
			get;
			set;
		}

		[XmlElement("lastmod", Order=2)]
		public string LastModificationDate
		{
			get;
			set;
		}

		[XmlElement("mobile", Order=8, Namespace="http://www.google.com/schemas/sitemap-mobile/1.0")]
		public SitemapMobile Mobile
		{
			get;
			set;
		}

		[XmlElement("news", Order=6, Namespace="http://www.google.com/schemas/sitemap-news/0.9")]
		public SitemapNews News
		{
			get;
			set;
		}

		[XmlElement("priority", Order=4)]
		public decimal? Priority
		{
			get;
			set;
		}

		[XmlElement("link", Order=9, Namespace="http://www.w3.org/1999/xhtml")]
		public List<SitemapPageTranslation> Translations
		{
			get;
			set;
		}

		[Url]
		[XmlElement("loc", Order=1)]
		public string Url
		{
			get;
			set;
		}

		[XmlElement("video", Order=7, Namespace="http://www.google.com/schemas/sitemap-video/1.1")]
		public SitemapVideo Video
		{
			get;
			set;
		}

		internal SitemapNode()
		{
		}

		public SitemapNode(string url)
		{
			Url = url;
		}

		public bool ShouldSerializeChangeFrequency()
		{
			return ChangeFrequency.HasValue;
		}

		public bool ShouldSerializeLastModificationDate()
		{
			return !string.IsNullOrEmpty(LastModificationDate);
		}

		public bool ShouldSerializePriority()
		{
			return Priority.HasValue;
		}
	}
}