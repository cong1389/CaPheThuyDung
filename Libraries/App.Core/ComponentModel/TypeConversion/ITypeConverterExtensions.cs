using System;
using System.Globalization;
using App.Core.Extensions;

namespace App.Core.ComponentModel.TypeConversion
{
    public static class ITypeConverterExtensions
    {
        public static object ConvertFrom(this ITypeConverter converter, object value)
        {
            return converter.ConvertFrom(CultureInfo.InvariantCulture, value);
        }

        public static object ConvertTo(this ITypeConverter converter, object value, Type to)
        {
            return converter.ConvertTo(CultureInfo.InvariantCulture, null, value, to);
        }

        public static object SafeConvert(this ITypeConverter converter, string value)
        {
            try
            {
                if (converter != null && value.HasValue() && converter.CanConvertFrom(typeof(string)))
                {
                    return converter.ConvertFrom(value);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        public static bool IsEqual(this ITypeConverter converter, string value, object compareWith)
        {
            object convertedObject = converter.SafeConvert(value);

            if (convertedObject != null && compareWith != null)
                return convertedObject.Equals(compareWith);

            return false;
        }
    }
}