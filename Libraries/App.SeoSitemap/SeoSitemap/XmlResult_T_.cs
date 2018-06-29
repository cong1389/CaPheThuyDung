using System.Text;
using System.Web;
using System.Web.Mvc;
using App.SeoSitemap.Common;
using App.SeoSitemap.Serialization;

namespace App.SeoSitemap
{
    internal class XmlResult<T> : ActionResult
	{
		private readonly IBaseUrlProvider _baseUrlProvider;

		private readonly T _data;

		private readonly IUrlValidator _urlValidator;

		internal XmlResult(T data, IUrlValidator urlValidator)
		{
			_data = data;
			_urlValidator = urlValidator;
		}

		internal XmlResult(T data, IBaseUrlProvider baseUrlProvider) : this(data, new UrlValidator(new ReflectionHelper()))
		{
			_baseUrlProvider = baseUrlProvider;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var urlValidator = _urlValidator;
			object obj = _data;
			object mvcBaseUrlProvider = _baseUrlProvider ?? new MvcBaseUrlProvider(context.HttpContext);

		    urlValidator.ValidateUrls(obj, (IBaseUrlProvider)mvcBaseUrlProvider);
			HttpResponseBase response = context.HttpContext.Response;
			response.ContentType = "text/xml";
			response.ContentEncoding = Encoding.UTF8;
			response.BufferOutput = false;
			new XmlSerializer().SerializeToStream(_data, response.OutputStream);
		}
	}
}