using System.Xml.Serialization;

namespace App.Aplication.SEO
{
	public class Location
	{
		[XmlElement("changefreq")]
		public EChangeFrequency? ChangeFrequency
		{
			get;
			set;
		}

		[XmlElement("lastmod")]
		public string LastModified
		{
			get;
			set;
		}

		[XmlElement("priority")]
		public double? Priority
		{
			get;
			set;
		}

		[XmlElement("loc")]
		public string Url
		{
			get;
			set;
		}

	    public bool ShouldSerializeChangeFrequency()
		{
			return ChangeFrequency.HasValue;
		}

		public bool ShouldSerializeLastModified()
		{
			return !string.IsNullOrEmpty(LastModified);
		}

		public bool ShouldSerializePriority()
		{
			return Priority.HasValue;
		}

		public enum EChangeFrequency
		{
			Always,
			Hourly,
			Daily,
			Weekly,
			Monthly,
			Yearly,
			Never
		}
	}
}