using System.Collections;
using System.Xml.Serialization;

namespace App.Aplication.SEO
{
	[XmlRoot("urlset", Namespace="http://www.sitemaps.org/schemas/sitemap/0.9")]
	public class Sitemap
	{
		private readonly ArrayList _map;

		[XmlElement("url")]
		public Location[] Locations
		{
			get
			{
				var locationArray = new Location[_map.Count];
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

		public Sitemap()
		{
			_map = new ArrayList();
		}

		public int Add(Location item)
		{
			return _map.Add(item);
		}
	}
}