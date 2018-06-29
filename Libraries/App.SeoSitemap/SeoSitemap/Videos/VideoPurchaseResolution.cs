using System.Xml.Serialization;

namespace App.SeoSitemap.Videos
{
    public enum VideoPurchaseResolution
	{
		None,
		[XmlEnum("hd")]
		Hd,
		[XmlEnum("sd")]
		Sd
	}
}