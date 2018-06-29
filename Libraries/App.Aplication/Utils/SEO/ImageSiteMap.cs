using System.Collections;
using System.Xml.Serialization;

namespace App.Aplication.SEO
{
    public class ImageSiteMap
	{
		private readonly ArrayList _map;

		[XmlElement("url")]
		public Location[] Locations
		{
			get
			{
				Location[] locationArray = new Location[_map.Count];
				_map.CopyTo(locationArray);
				return locationArray;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				_map.Clear();
				var locationArray = value;
				foreach (var location in locationArray)
				{
				    _map.Add(location);
				}
			}
		}

		public ImageSiteMap()
		{
			_map = new ArrayList();
		}

		public class ImageInfo
		{
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

		    public bool ShouldSerializeLastModified()
			{
				return !string.IsNullOrEmpty(LastModified);
			}

			public bool ShouldSerializePriority()
			{
				return Priority.HasValue;
			}
		}
	}
}