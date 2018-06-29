using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace App.Core.Utilities
{
    public static class ExpressionOrderBy
	{
		private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
		{
			var strArrays = property.Split('.');
			var propertyType = typeof(T);
			var parameterExpression = Expression.Parameter(propertyType, "x");
			Expression expression = parameterExpression;
			//var strArrays1 = strArrays;

			for (var i = 0; i < strArrays.Length; i++)
			{
				var propertyInfo = propertyType.GetProperty(strArrays[i]);
				expression = Expression.Property(expression, propertyInfo);
				propertyType = propertyInfo.PropertyType;
			}

			var type = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
			var lambdaExpression = Expression.Lambda(type, expression, parameterExpression);

			return (IOrderedQueryable<T>) typeof(Queryable).GetMethods()
			    .Single(method =>
			        method.Name == methodName && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 &&
			        method.GetParameters().Length == 2).MakeGenericMethod(typeof(T), propertyType)
			    .Invoke(null, new object[] {source, lambdaExpression});
		}

		public static object GetPropertyValue(object obj, string property)
		{
			var propertyInfo = obj.GetType().GetProperty(property);

			return propertyInfo.GetValue(obj, null);
		}

		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "OrderBy");
		}

		public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "OrderByDescending");
		}

		public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "ThenBy");
		}

		public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
		{
			return ApplyOrder(source, property, "ThenByDescending");
		}
	}
}