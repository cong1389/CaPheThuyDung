using System.Xml.Serialization;

namespace App.SeoSitemap.Videos
{
    public enum VideoPurchaseOption
	{
		None,
		[XmlEnum("rent")]
		Rent,
		[XmlEnum("own")]
		Own
	}
}