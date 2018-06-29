using System.Xml.Serialization;

namespace App.SeoSitemap.Enum
{
    public enum YesNo
	{
		None,
		[XmlEnum("yes")]
		Yes,
		[XmlEnum("no")]
		No
	}
}