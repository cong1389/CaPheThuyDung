using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace App.Front
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
	}
}