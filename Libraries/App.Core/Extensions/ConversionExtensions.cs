using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using App.Core.ComponentModel.TypeConversion;

namespace App.Core.Extensions
{
	public static class ConversionExtensions
	{
		public static string AsString(this Stream stream)
		{
			return stream.AsString(Encoding.UTF8);
		}

	    public static T Convert<T>(this object value)
	    {
	        return (T)(Convert(value, typeof(T)) ?? default(T));
	    }

	    public static T Convert<T>(this object value, T defaultValue)
	    {
	        return (T)(Convert(value, typeof(T)) ?? defaultValue);
	    }

	    public static T Convert<T>(this object value, CultureInfo culture)
	    {
	        return (T)(Convert(value, typeof(T), culture) ?? default(T));
	    }

	    public static T Convert<T>(this object value, T defaultValue, CultureInfo culture)
	    {
	        return (T)(Convert(value, typeof(T), culture) ?? defaultValue);
	    }

	    public static object Convert(this object value, Type to)
	    {
	        return value.Convert(to, CultureInfo.InvariantCulture);
	    }

        public static string AsString(this Stream stream, Encoding encoding)
		{
			// convert stream to string
			string result;

			if (stream.CanSeek)
			{
				stream.Position = 0;
			}

			using (StreamReader sr = new StreamReader(stream, encoding))
			{
				result = sr.ReadToEnd();
			}

			return result;
		}

		public static object Convert(this object value, Type to, CultureInfo culture)
		{
			if (value == null || value == DBNull.Value || to.IsInstanceOfType(value))
			{
				return value == DBNull.Value ? null : value;
			}

			Type from = value.GetType();

			if (culture == null)
			{
				culture = CultureInfo.InvariantCulture;
			}

			// get a converter for 'to' (value -> to)
			var converter = TypeConverterFactory.GetConverter(to);
			if (converter != null && converter.CanConvertFrom(from))
			{
				return converter.ConvertFrom(culture, value);
			}

			// try the other way round with a 'from' converter (to <- from)
			converter = TypeConverterFactory.GetConverter(from);
			if (converter != null && converter.CanConvertTo(to))
			{
				return converter.ConvertTo(culture, null, value, to);
			}

			// use Convert.ChangeType if both types are IConvertible
			if (value is IConvertible && typeof(IConvertible).IsAssignableFrom(to))
			{
				return System.Convert.ChangeType(value, to, culture);
			}

			throw null;
		}

		[DebuggerStepThrough]
		public static Version ToVersion(this string value, Version defaultVersion = null)
		{
			try
			{
				return new Version(value);
			}
			catch
			{
				return defaultVersion ?? new Version("1.0");
			}
		}
	}
}
