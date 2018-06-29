using System;

namespace App.SeoSitemap.Common
{
	public interface IBaseUrlProvider
	{
		Uri BaseUrl
		{
			get;
		}
	}
}