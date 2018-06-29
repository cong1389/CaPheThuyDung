using System.Xml.Serialization;
using App.SeoSitemap.Common;

namespace App.SeoSitemap.Videos
{
    public class VideoGallery
	{
		[XmlAttribute("title")]
		public string Title
		{
			get;
			set;
		}

		[Url]
		[XmlText]
		public string Url
		{
			get;
			set;
		}

		internal VideoGallery()
		{
		}

		public VideoGallery(string url)
		{
			Url = url;
		}
	}
}