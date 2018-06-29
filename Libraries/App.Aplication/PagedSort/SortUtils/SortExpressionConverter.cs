using System;
using System.ComponentModel;
using System.Globalization;

namespace App.Aplication.PagedSort.SortUtils
{
    public class SortExpressionConverter : TypeConverter
	{
		public const char SortExpressionFieldDelimiter = ',';

	    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return new SortExpression();
			}
			string str = (string) value;
		    if (str.Length < 1)
			{
				return new SortExpression();
			}
			string[] strArrays = str.Split(',');
			if (strArrays.Length != 3)
			{
				throw new Exception("Invalid data format!");
			}
			string str1 = strArrays[0];
			string str2 = strArrays[1];
			SortDirection sortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), strArrays[2], true);
			return new SortExpression(str1, str2, sortDirection);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value != null && !(value is SortExpression))
			{
				throw new Exception($"Unable to convert type '{value.GetType()}'!");
			}
			if (destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			SortExpression sortExpression = (SortExpression) value;
			return $"{sortExpression.Title}{','}{sortExpression.Expression}{','}{sortExpression.Direction}";
		}
	}
}