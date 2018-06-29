using System.Xml.Serialization;

namespace App.Aplication.SEO
{
	[XmlRoot("urlset", ElementName="image", Namespace="http://www.google.com/schemas/sitemap-image/1.1")]
	public class SiteMapImage
	{
		[XmlElement(Type=typeof(ImageSiteMap), Namespace="http://www.sitemaps.org/schemas/sitemap/0.9")]
		public SiteMapImage MapImage
		{
			get;
			set;
		}
	}
}