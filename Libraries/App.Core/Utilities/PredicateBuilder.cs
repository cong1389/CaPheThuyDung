using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace App.Core.Utilities
{
    public static class PredicateBuilder
	{
		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			var expression =
			    first.Compose(second, Expression.AndAlso);

		    return expression;
		}

		private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
		{
			var dictionary = first.Parameters
			    .Select((f, i) => new {f, s = second.Parameters[i]}).ToDictionary(p => p.s, p => p.f);
			var expression = ParameterRebinder.ReplaceParameters(dictionary, second.Body);
			var expression1 = Expression.Lambda<T>(merge(first.Body, expression), first.Parameters);

		    return expression1;
		}

		public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
		{
			return predicate;
		}

		public static Expression<Func<T, bool>> False<T>()
		{
			return param => false;
		}

		public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
		{
			var unaryExpression = Expression.Not(expression.Body);
			return Expression.Lambda<Func<T, bool>>(unaryExpression, expression.Parameters);
		}

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
		{
			Expression<Func<T, bool>> expression = first.Compose(second, Expression.OrElse);
			return expression;
		}

		public static Expression<Func<T, bool>> True<T>()
		{
			return param => true;
		}

		private class ParameterRebinder : ExpressionVisitor
		{
			private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

			private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
			{
				_map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
			}

			public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
			{
				return new ParameterRebinder(map).Visit(exp);
			}

			protected override Expression VisitParameter(ParameterExpression p)
			{
			    if (_map.TryGetValue(p, out var parameterExpression))
				{
					p = parameterExpression;
				}

				return base.VisitParameter(p);
			}
		}
	}
}