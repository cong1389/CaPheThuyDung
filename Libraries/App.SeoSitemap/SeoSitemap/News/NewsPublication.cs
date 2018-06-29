using System.Xml.Serialization;

namespace App.SeoSitemap.News
{
    [XmlRoot("url", Namespace="http://www.google.com/schemas/sitemap-news/0.9")]
	public class NewsPublication
	{
		[XmlElement("language")]
		public string Language
		{
			get;
			set;
		}

		[XmlElement("name")]
		public string Name
		{
			get;
			set;
		}

		internal NewsPublication()
		{
		}

		public NewsPublication(string name, string language)
		{
			Name = name;
			Language = language;
		}
	}
}