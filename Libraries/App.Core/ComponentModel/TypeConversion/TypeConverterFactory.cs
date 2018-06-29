using App.Core.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace App.Core.ComponentModel.TypeConversion
{
    public class TypeConverterFactory
	{
		private static readonly ConcurrentDictionary<Type, ITypeConverter> _typeConverters = new ConcurrentDictionary<Type, ITypeConverter>();

		//static TypeConverterFactory()
		//{
		//	CreateDefaultConverters();
		//}

		//private static void CreateDefaultConverters()
		//{
		//	_typeConverters.TryAdd(typeof(EmailAddress), new EmailAddressConverter());
		//}
		
	    public static void RegisterConverter<T>(ITypeConverter typeConverter)
	    {
	        RegisterConverter(typeof(T), typeConverter);
	    }

	    public static void RegisterConverter(Type type, ITypeConverter typeConverter)
	    {
	        _typeConverters.TryAdd(type, typeConverter);
	    }

		public static ITypeConverter GetConverter<T>()
		{
			return GetConverter(typeof(T));
		}

		public static ITypeConverter GetConverter(object component)
		{
			return GetConverter(component.GetType());
		}

		public static ITypeConverter GetConverter(Type type)
		{
			if (_typeConverters.TryGetValue(type, out var converter))
			{
				return converter;
			}

			var isGenericType = type.IsGenericType;
			if (isGenericType)
			{
				var definition = type.GetGenericTypeDefinition();

				// Nullables
				if (definition == typeof(Nullable<>))
				{
					converter = new NullableConverter(type);
					RegisterConverter(type, converter);
					return converter;
				}

				// Sequence types
				var genericArgs = type.GetGenericArguments();
				var isEnumerable = genericArgs.Length == 1 && type.IsSubClass(typeof(IEnumerable<>));
				if (isEnumerable)
				{
					converter = (ITypeConverter)Activator.CreateInstance(typeof(EnumerableConverter<>).MakeGenericType(genericArgs[0]), type);
					RegisterConverter(type, converter);
					return converter;
				}
			}

			// default fallback
			converter = new TypeConverterAdapter(TypeDescriptor.GetConverter(type));
			RegisterConverter(type, converter);
			return converter;
		}
	}
}
