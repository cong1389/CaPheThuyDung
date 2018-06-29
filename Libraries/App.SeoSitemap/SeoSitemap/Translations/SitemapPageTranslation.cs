using System.Xml.Serialization;
using App.SeoSitemap.Common;

namespace App.SeoSitemap.Translations
{
    public class SitemapPageTranslation
	{
		[XmlAttribute("hreflang")]
		public string Language
		{
			get;
			set;
		}

		[XmlAttribute("rel")]
		public string Rel
		{
			get;
			set;
		}

		[Url]
		[XmlAttribute("href")]
		public string Url
		{
			get;
			set;
		}

		internal SitemapPageTranslation()
		{
		}

		public SitemapPageTranslation(string url, string language, string rel = "alternate")
		{
			Url = url;
			Language = language;
			Rel = rel;
		}
	}
}