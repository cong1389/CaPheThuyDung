using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace App.Core.Utilities
{
	public class PredicateExtention
	{
		private static readonly MethodInfo StringContainsMethod;

		private static readonly MethodInfo BooleanEqualsMethod;

		static PredicateExtention()
		{
			StringContainsMethod = typeof(string).GetMethod("Contains", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null);
			BooleanEqualsMethod = typeof(bool).GetMethod("Equals", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(bool) }, null);
		}

	    private static Expression<Func<TDbType, bool>> ApplyBoolCriterion<TDbType, TSearchCriteria>(
		    TSearchCriteria searchCriteria, PropertyInfo searchCriterionPropertyInfo, Type dbType,
		    MemberInfo dbFieldMemberInfo, Expression<Func<TDbType, bool>> predicate)
		{
			Expression<Func<TDbType, bool>> expression;
		    if (searchCriterionPropertyInfo.GetValue(searchCriteria) is bool value)
			{
				var parameterExpression = Expression.Parameter(dbType, "x");
				var memberExpression = Expression.MakeMemberAccess(parameterExpression, dbFieldMemberInfo);
				Expression[] expressionArray = { Expression.Constant(value) };
				var methodCallExpression = Expression.Call(memberExpression, BooleanEqualsMethod, expressionArray);
				var expression1 = Expression.Lambda(methodCallExpression, parameterExpression) as Expression<Func<TDbType, bool>>;
				expression = predicate.And(expression1);
			}
			else
			{
				expression = predicate;
			}
			return expression;
		}

		private static Expression<Func<TDbType, bool>> ApplyStringCriterion<TDbType, TSearchCriteria>(TSearchCriteria searchCriteria, PropertyInfo searchCriterionPropertyInfo, Type dbType, MemberInfo dbFieldMemberInfo, Expression<Func<TDbType, bool>> predicate)
		{
			Expression<Func<TDbType, bool>> expression;
			var value = searchCriterionPropertyInfo.GetValue(searchCriteria) as string;
			if (!string.IsNullOrWhiteSpace(value))
			{
				var parameterExpression = Expression.Parameter(dbType, "x");
				var memberExpression = Expression.MakeMemberAccess(parameterExpression, dbFieldMemberInfo);
				Expression[] expressionArray = { Expression.Constant(value) };
				var methodCallExpression = Expression.Call(memberExpression, StringContainsMethod, expressionArray);
				var expression1 = Expression.Lambda(methodCallExpression, parameterExpression) as Expression<Func<TDbType, bool>>;
				expression = predicate.And(expression1);
			}
			else
			{
				expression = predicate;
			}
			return expression;
		}

		public static Expression<Func<TDbType, bool>> BuildPredicate<TDbType, TSearchCriteria>(TSearchCriteria searchCriteria)
		{
			var expression = PredicateBuilder.True<TDbType>();
			var properties = searchCriteria.GetType().GetProperties();
			for (var i = 0; i < properties.Length; i++)
			{
				var propertyInfo = properties[i];
				var dbFieldName = GetDbFieldName(propertyInfo);
				var type = typeof(TDbType);
				var memberInfo = type.GetMember(dbFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public).Single();
				if (propertyInfo.PropertyType == typeof(string))
				{
					expression = ApplyStringCriterion(searchCriteria, propertyInfo, type, memberInfo, expression);
				}
				else if (propertyInfo.PropertyType == typeof(bool?))
				{
					expression = ApplyBoolCriterion(searchCriteria, propertyInfo, type, memberInfo, expression);
				}
			}
			return expression;
		}

		private static string GetDbFieldName(PropertyInfo propertyInfo)
		{
			var obj = propertyInfo.GetCustomAttributes(typeof(DbFieldMapAttribute), false).FirstOrDefault();
			return obj != null ? ((DbFieldMapAttribute)obj).Field : propertyInfo.Name;
		}
	}
}