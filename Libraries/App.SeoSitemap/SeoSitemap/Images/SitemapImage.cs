using System.Xml.Serialization;
using App.SeoSitemap.Common;

namespace App.SeoSitemap.Images
{
    public class SitemapImage
	{
		[XmlElement("caption", Order=2)]
		public string Caption
		{
			get;
			set;
		}

		[Url]
		[XmlElement("license", Order=5)]
		public string License
		{
			get;
			set;
		}

		[XmlElement("geo_location", Order=3)]
		public string Location
		{
			get;
			set;
		}

		[XmlElement("title", Order=4)]
		public string Title
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

		internal SitemapImage()
		{
		}

		public SitemapImage(string url)
		{
			Url = url;
		}
	}
}