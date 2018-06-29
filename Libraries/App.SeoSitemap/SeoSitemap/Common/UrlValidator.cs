using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace App.SeoSitemap.Common
{
    internal class UrlValidator : IUrlValidator
	{
		private readonly IReflectionHelper _reflectionHelper;

		private readonly Dictionary<Type, UrlPropertyModel> _propertyModelList;

		public UrlValidator(IReflectionHelper reflectionHelper)
		{
			_reflectionHelper = reflectionHelper;
			_propertyModelList = new Dictionary<Type, UrlPropertyModel>();
		}

		private void CheckForRelativeUrls(object item, PropertyInfo propertyInfo, IBaseUrlProvider baseUrlProvider)
		{
			object value = propertyInfo.GetValue(item, null);
			if (value != null)
			{
				string str = value.ToString();
				if (!Uri.IsWellFormedUriString(str, UriKind.Absolute) && Uri.IsWellFormedUriString(str, UriKind.Relative))
				{
					string str1 = $"{baseUrlProvider.BaseUrl.ToString().TrimEnd('/')}/{str.TrimStart('/')}";
					propertyInfo.SetValue(item, str1, null);
				}
			}
		}

		private UrlPropertyModel GetPropertyModel(Type type)
		{
		    if (!_propertyModelList.TryGetValue(type, out var propertyModel))
			{
				propertyModel = _reflectionHelper.GetPropertyModel(type);
				_propertyModelList[type] = propertyModel;
			}
			return propertyModel;
		}

		public void ValidateUrls(object item, IBaseUrlProvider baseUrlProvider)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (baseUrlProvider == null)
			{
				throw new ArgumentNullException("baseUrlProvider");
			}
			UrlPropertyModel propertyModel = GetPropertyModel(item.GetType());
			foreach (PropertyInfo urlProperty in propertyModel.UrlProperties)
			{
				CheckForRelativeUrls(item, urlProperty, baseUrlProvider);
			}
			foreach (PropertyInfo classProperty in propertyModel.ClassProperties)
			{
				object value = classProperty.GetValue(item, null);
				if (value == null)
				{
					continue;
				}
				ValidateUrls(value, baseUrlProvider);
			}
			foreach (PropertyInfo enumerableProperty in propertyModel.EnumerableProperties)
			{
				IEnumerable enumerable = enumerableProperty.GetValue(item, null) as IEnumerable;
				if (enumerable == null)
				{
					continue;
				}
				foreach (object obj in enumerable)
				{
					ValidateUrls(obj, baseUrlProvider);
				}
			}
		}
	}
}