using System;
using System.Web;

namespace App.SeoSitemap.Common
{
	internal class MvcBaseUrlProvider : IBaseUrlProvider
	{
		private readonly HttpContextBase _httpContext;

		public Uri BaseUrl => new Uri(
		    $"{_httpContext.Request.Url.Scheme}://{_httpContext.Request.Url.Authority}{_httpContext.Request.ApplicationPath}");

	    public MvcBaseUrlProvider(HttpContextBase httpContext)
		{
			_httpContext = httpContext;
		}
	}
}