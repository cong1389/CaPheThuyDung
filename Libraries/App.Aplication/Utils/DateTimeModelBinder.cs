using System;
using System.Globalization;
using System.Web.Mvc;

namespace App.Aplication
{
    public class DateTimeModelBinder : DefaultModelBinder
	{
	    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
		    var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (string.IsNullOrEmpty(value?.AttemptedValue))
			{
				return base.BindModel(controllerContext, bindingContext);
			}
			const string str = "MM/dd/yyyy";
			DateTime.TryParseExact(value.AttemptedValue, str, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime);
			return dateTime;
		}
	}
}