using System.Xml.Serialization;

namespace App.SeoSitemap.Enum
{
    public enum ChangeFrequency
	{
		[XmlEnum("always")]
		Always,
		[XmlEnum("hourly")]
		Hourly,
		[XmlEnum("daily")]
		Daily,
		[XmlEnum("weekly")]
		Weekly,
		[XmlEnum("monthly")]
		Monthly,
		[XmlEnum("yearly")]
		Yearly,
		[XmlEnum("never")]
		Never
	}
}