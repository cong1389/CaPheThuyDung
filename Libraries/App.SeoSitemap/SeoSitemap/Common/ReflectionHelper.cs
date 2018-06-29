using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace App.SeoSitemap.Common
{
    internal class ReflectionHelper : IReflectionHelper
	{
		public ReflectionHelper()
		{
		}

		public virtual UrlPropertyModel GetPropertyModel(Type type)
		{
			UrlPropertyModel urlPropertyModel = new UrlPropertyModel();
			PropertyInfo[] properties = type.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.GetCustomAttributes(typeof(UrlAttribute), true).Any() && propertyInfo.PropertyType == typeof(string) && propertyInfo.CanRead && propertyInfo.CanWrite)
				{
					urlPropertyModel.UrlProperties.Add(propertyInfo);
				}
				else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) && propertyInfo.CanRead)
				{
					urlPropertyModel.EnumerableProperties.Add(propertyInfo);
				}
				else if (propertyInfo.PropertyType.GetTypeInfo().IsClass && propertyInfo.PropertyType != typeof(string) && propertyInfo.CanRead)
				{
					urlPropertyModel.ClassProperties.Add(propertyInfo);
				}
			}
			return urlPropertyModel;
		}
	}
}