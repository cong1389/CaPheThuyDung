using System;
using System.Globalization;
using System.Web.Mvc;

namespace App.Aplication
{
	public class DoubleModelBinder : DefaultModelBinder
	{
	    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (value == null)
			{
				return base.BindModel(controllerContext, bindingContext);
			}
			return Convert.ToDouble(value.AttemptedValue, CultureInfo.GetCultureInfo("en-US"));
		}
	}
}