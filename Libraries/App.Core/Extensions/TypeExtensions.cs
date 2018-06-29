	using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace App.Core.Extensions
{
    public static class TypeExtensions
	{
		private static readonly Type[] SPredefinedTypes = { typeof(string), typeof(decimal), typeof(DateTime), typeof(TimeSpan), typeof(Guid) };
		private static readonly Type[] SPredefinedGenericTypes = { typeof(Nullable<>) };

		public static string AssemblyQualifiedNameWithoutVersion(this Type type)
		{
			if (type.AssemblyQualifiedName != null)
			{
				var strArray = type.AssemblyQualifiedName.Split(',');
				return $"{strArray[0].Trim()}, {strArray[1].Trim()}";
			}

			return null;
		}

		public static bool IsSequenceType(this Type type)
		{
			if (type == typeof(string))
				return false;

			return type.IsArray || typeof(IEnumerable).IsAssignableFrom(type);
		}

		public static bool IsPredefinedSimpleType(this Type type)
		{
			if (type.IsPrimitive && type != typeof(IntPtr) && type != typeof(UIntPtr))
			{
				return true;
			}

			if (type.IsEnum)
			{
				return true;
			}

			return SPredefinedTypes.Any(t => t == type);
		}

		public static bool IsStruct(this Type type)
		{
			if (type.IsValueType)
			{
				return !type.IsPredefinedSimpleType();
			}

			return false;
		}

		public static bool IsPredefinedGenericType(this Type type)
		{
			if (type.IsGenericType)
			{
				type = type.GetGenericTypeDefinition();
			}
			else
			{
				return false;
			}

			return SPredefinedGenericTypes.Any(t => t == type);
		}

		public static bool IsPredefinedType(this Type type)
		{
			if (!IsPredefinedSimpleType(type) && !IsPredefinedGenericType(type) && type != typeof(byte[]))
			{
				return string.Compare(type.FullName, "System.Xml.Linq.XElement", StringComparison.Ordinal) == 0;
			}

			return true;
		}

		public static bool IsPlainObjectType(this Type type)
		{
			return type.IsClass && !type.IsSequenceType() && !type.IsPredefinedType();
		}

		public static bool IsInteger(this Type type)
		{
			switch (Type.GetTypeCode(type))
			{
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return true;
				default:
					return false;
			}
		}

		public static bool IsNullable(this Type type, out Type wrappedType)
		{
			wrappedType = null;

			if (type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				wrappedType = type.GetGenericArguments()[0];
			}

			return false;
		}

		public static bool IsConstructable(this Type type)
		{
			if (type.IsAbstract || type.IsInterface || type.IsArray || type.IsGenericTypeDefinition || type == typeof(void))
				return false;

			if (!HasDefaultConstructor(type))
				return false;

			return true;
		}

		[DebuggerStepThrough]
		public static bool IsAnonymous(this Type type)
		{
			if (type.IsGenericType)
			{
				var d = type.GetGenericTypeDefinition();
				if (d.IsClass && d.IsSealed && d.Attributes.HasFlag(TypeAttributes.NotPublic))
				{
					var attributes = d.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
					if (attributes.Length > 0)
					{
						//WOW! We have an anonymous type!!!
						return true;
					}
				}
			}

			return false;
		}

		[DebuggerStepThrough]
		public static bool HasDefaultConstructor(this Type type)
		{
			if (type.IsValueType)
				return true;

			return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
				.Any(ctor => ctor.GetParameters().Length == 0);
		}

		public static bool IsSubClass(this Type type, Type check)
		{
		    return IsSubClass(type, check, out _);
		}

		public static bool IsSubClass(this Type type, Type check, out Type implementingType)
		{
			return IsSubClassInternal(type, type, check, out implementingType);
		}

		private static bool IsSubClassInternal(Type initialType, Type currentType, Type check, out Type implementingType)
		{
			if (currentType == check)
			{
				implementingType = currentType;
				return true;
			}

			// don't get interfaces for an interface unless the initial type is an interface
			if (check.IsInterface && (initialType.IsInterface || currentType == initialType))
			{
				foreach (Type t in currentType.GetInterfaces())
				{
					if (IsSubClassInternal(initialType, t, check, out implementingType))
					{
						// don't return the interface itself, return it's implementor
						if (check == implementingType)
							implementingType = currentType;

						return true;
					}
				}
			}

			if (currentType.IsGenericType && !currentType.IsGenericTypeDefinition)
			{
				if (IsSubClassInternal(initialType, currentType.GetGenericTypeDefinition(), check, out implementingType))
				{
					implementingType = currentType;
					return true;
				}
			}

			if (currentType.BaseType == null)
			{
				implementingType = null;
				return false;
			}

			return IsSubClassInternal(initialType, currentType.BaseType, check, out implementingType);
		}

		/// <summary>
		/// Gets the underlying type of a <see cref="Nullable{T}" /> type.
		/// </summary>
		public static Type GetNonNullableType(this Type type)
		{
			if (!IsNullable(type, out var wrappedType))
			{
				return type;
			}

			return wrappedType;
		}

		public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits) where TAttribute : Attribute
		{
			if (target.IsDefined(typeof(TAttribute), inherits))
			{
				var attributes = target.GetCustomAttributes(typeof(TAttribute), inherits);
				if (attributes.Length > 1)
				{
					throw null;
				}
				return (TAttribute)attributes[0];
			}

			return null;
		}

		public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits) where TAttribute : Attribute
		{
			return target.IsDefined(typeof(TAttribute), inherits);
		}

		//[SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
		//internal static IEnumerable<TAttribute> SortAttributesIfPossible<TAttribute>(IEnumerable<TAttribute> attributes)
		//	where TAttribute : Attribute
		//{
		//	if (typeof(IOrdered).IsAssignableFrom(typeof(TAttribute)))
		//	{
		//		return attributes.Cast<IOrdered>().OrderBy(x => x.Ordinal).Cast<TAttribute>();
		//	}

		//	return attributes;
		//}

		internal static Type FindIEnumerable(this Type seqType)
		{
			if (seqType == null || seqType == typeof(string))
				return null;

			if (seqType.IsArray)
				return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

			if (seqType.IsGenericType)
			{
				var args = seqType.GetGenericArguments();
				foreach (var arg in args)
				{
					var ienum = typeof(IEnumerable<>).MakeGenericType(arg);
					if (ienum.IsAssignableFrom(seqType))
						return ienum;
				}
			}

			Type[] ifaces = seqType.GetInterfaces();
			if (ifaces.Length > 0)
			{
				foreach (Type iface in ifaces)
				{
					Type ienum = FindIEnumerable(iface);
					if (ienum != null)
						return ienum;
				}
			}

			if (seqType.BaseType != null && seqType.BaseType != typeof(object))
				return FindIEnumerable(seqType.BaseType);

			return null;
		}
	}
}
