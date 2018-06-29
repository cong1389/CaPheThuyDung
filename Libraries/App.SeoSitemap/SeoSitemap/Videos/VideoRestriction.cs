using System.Xml.Serialization;

namespace App.SeoSitemap.Videos
{
    public class VideoRestriction
	{
		[XmlText]
		public string Countries
		{
			get;
			set;
		}

		[XmlAttribute("relationship")]
		public VideoRestrictionRelationship Relationship
		{
			get;
			set;
		}

		internal VideoRestriction()
		{
		}

		public VideoRestriction(string countries, VideoRestrictionRelationship relationship)
		{
			Countries = countries;
			Relationship = relationship;
		}
	}
}