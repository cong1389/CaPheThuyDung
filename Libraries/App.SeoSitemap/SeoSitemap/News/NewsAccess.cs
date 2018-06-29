using System.Xml.Serialization;

namespace App.SeoSitemap.News
{
    public enum NewsAccess
	{
		[XmlEnum]
		Subscription,
		[XmlEnum]
		Registration
	}
}