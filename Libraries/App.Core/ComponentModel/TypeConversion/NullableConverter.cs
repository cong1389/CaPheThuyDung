﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace App.Core.ComponentModel.TypeConversion
{
    [SuppressMessage("ReSharper", "TryCastAlwaysSucceeds")]
    public class NullableConverter : TypeConverterBase
	{
        private readonly bool _underlyingTypeIsConvertible;

        public NullableConverter(Type type)
            : base(type)
        {
            NullableType = type;
            UnderlyingType = Nullable.GetUnderlyingType(type);

            if (UnderlyingType == null)
            {
				throw new ArgumentException("type", "Type is not a nullable type.");
			}

            _underlyingTypeIsConvertible = typeof(IConvertible).IsAssignableFrom(UnderlyingType) && !UnderlyingType.IsEnum;
            UnderlyingTypeConverter = TypeConverterFactory.GetConverter(UnderlyingType);
        }

        public Type NullableType
        {
            get;
            private set;
        }

        public Type UnderlyingType
        {
            get;
            private set;
        }

        public ITypeConverter UnderlyingTypeConverter
        {
            get;
            private set;
        }

        public override bool CanConvertFrom(Type type)
        {
            if (type == UnderlyingType)
            {
                return true;
            }

            if (UnderlyingTypeConverter.CanConvertFrom(type))
            {
                return true;
            }

            if (_underlyingTypeIsConvertible && type != typeof(string) && typeof(IConvertible).IsAssignableFrom(type))
            {
                return true;
            }

            return false;
        }

        public override bool CanConvertTo(Type type)
        {
            //Console.WriteLine("NullableConverter can convert to {0}: {1}".FormatInvariant(type.Name, UnderlyingTypeConverter.CanConvertTo(type)));

            if (type == UnderlyingType)
            {
                return true;
            }

            return UnderlyingTypeConverter.CanConvertTo(type);
        }

        public override object ConvertFrom(CultureInfo culture, object value)
        {
            if ((value == null) || (value.GetType() == UnderlyingType))
            {
                return value;
            }

            if ((value is string) && string.IsNullOrEmpty(value as string))
            {
                return null;
            }

            if (_underlyingTypeIsConvertible && !(value is string) && value is IConvertible)
            {
                // num > num?
                return Convert.ChangeType(value, UnderlyingType, culture);
            }

            return UnderlyingTypeConverter.ConvertFrom(culture, value);
        }

        public override object ConvertTo(CultureInfo culture, string format, object value, Type to)
        {
            if ((to == UnderlyingType) && NullableType.IsInstanceOfType(value))
            {
                return value;
            }

            if (value == null)
            {
                if (to == typeof(string))
                {
                    return string.Empty;
                }
            }

            return UnderlyingTypeConverter.ConvertTo(culture, format, value, to);
        }
    }
}
