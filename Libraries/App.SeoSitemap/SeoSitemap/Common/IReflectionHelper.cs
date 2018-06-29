using System;

namespace App.SeoSitemap.Common
{
	internal interface IReflectionHelper
	{
		UrlPropertyModel GetPropertyModel(Type type);
	}
}