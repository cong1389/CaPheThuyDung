using System.Xml.Serialization;

namespace App.SeoSitemap.Videos
{
    public enum VideoRestrictionRelationship
	{
		[XmlEnum("allow")]
		Allow,
		[XmlEnum("deny")]
		Deny
	}
}