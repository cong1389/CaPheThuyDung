using System.Xml.Serialization;
using App.SeoSitemap.Common;

namespace App.SeoSitemap.Videos
{
    public class VideoUploader
	{
		[Url]
		[XmlAttribute("info")]
		public string Info
		{
			get;
			set;
		}

		[XmlText]
		public string Name
		{
			get;
			set;
		}

		internal VideoUploader()
		{
		}

		public VideoUploader(string name)
		{
			Name = name;
		}
	}
}