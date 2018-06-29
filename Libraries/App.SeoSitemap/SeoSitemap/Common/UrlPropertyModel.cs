using System.Collections.Generic;
using System.Reflection;

namespace App.SeoSitemap.Common
{
    internal class UrlPropertyModel
	{
		public List<PropertyInfo> ClassProperties
		{
			get;
		}

		public List<PropertyInfo> EnumerableProperties
		{
			get;
		}

		public List<PropertyInfo> UrlProperties
		{
			get;
		}

		public UrlPropertyModel()
		{
			UrlProperties = new List<PropertyInfo>();
			EnumerableProperties = new List<PropertyInfo>();
			ClassProperties = new List<PropertyInfo>();
		}
	}
}