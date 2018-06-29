using App.Framework.Templating.Liquid;
using App.Service.Addresses;
using DotLiquid;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace App.Framework.Templating.Filters
{
    public static class AdditionalFilters
	{
		private static LiquidTemplateEngine GetEngine()
		{
			return (LiquidTemplateEngine)Template.FileSystem;
		}

		#region Common Filters

		public static object Default(object input, object value)
		{
			return input ?? value;
		}

		public static string Json(object input)
		{
			if (input == null)
				return null;

			return JsonConvert.SerializeObject(input, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}

		public static string FormatAddress(Context context, object input)
		{
			if (input == null)
				return null;

			var engine = GetEngine();
            //var countryService = engine.Services.Resolve<ICountryService>();

		    var addressService = DependencyResolver.Current.GetService<IAddressService>();
            //var addressService = engine.Services.Resolve<IAddressService>();

			//Country country = null;

			// We know that we converted Address entity to a dictionary.

			//if (input is IDictionary<string, object> dict)
			//{
			//	country = countryService.GetCountryById(dict.Get("CountryId").Convert<int?>().GetValueOrDefault());
			//}
			//else if (input is IIndexable lq)
			//{
			//	country = countryService.GetCountryById(lq["CountryId"].Convert<int?>().GetValueOrDefault());
			//}

			//return addressService.FormatAddress(input, country?.AddressFormat, context.FormatProvider);
		    return string.Empty;
		}

		#endregion

		#region Array Filters

		public static IEnumerable Uniq(IEnumerable input)
		{
		    return input?.Cast<object>().Distinct();
		}

		public static IEnumerable Compact(IEnumerable input)
		{
		    return input?.Cast<object>().Where(x => x != null);
		}

		public static IEnumerable Reverse(IEnumerable input)
		{
		    return input?.Cast<object>().Reverse();
		}

		#endregion
        
		#region Url Filters

		#endregion

		#region Html Filters

		public static string ScriptTag(string input)
		{
			return $"<script src=\"{input}\"></script>";
		}

		public static string StylesheetTag(string input)
		{
			return $"<link href=\"{input}\" rel=\"stylesheet\" type=\"text/css\" media=\"all\" />";
		}

		public static string ImgTag(string input, string alt = "", string css = "")
		{
			return input == null ? null : GetImageTag(input, alt, css);
		}

		private static string GetImageTag(string src, string alt, string css)
		{
			return $"<img src=\"{src}\" alt=\"{alt}\" class=\"{css}\" />";
		}

		#endregion
	}
}
