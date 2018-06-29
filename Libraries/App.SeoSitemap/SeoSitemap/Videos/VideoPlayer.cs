using System.Xml.Serialization;
using App.SeoSitemap.Common;
using App.SeoSitemap.Enum;

namespace App.SeoSitemap.Videos
{
    public class VideoPlayer
	{
		[XmlAttribute("allow_embed")]
		public YesNo AllowEmbed
		{
			get;
			set;
		}

		[XmlAttribute("autoplay")]
		public string Autoplay
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

		internal VideoPlayer()
		{
		}

		public VideoPlayer(string url)
		{
			Url = url;
		}

		public bool ShouldSerializeAllowEmbed()
		{
			return AllowEmbed != YesNo.None;
		}
	}
}