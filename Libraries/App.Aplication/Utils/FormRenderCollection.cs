using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace App.Aplication
{
	public static class FormRenderCollection
	{
		private static MvcHtmlString _EditorForManyIndexField<TModel>(string htmlFieldNameWithPrefix, string guid, Expression<Func<TModel, string>> indexResolverExpression)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(@"<input type=""hidden"" name=""{0}.Index"" value=""{1}"" />", htmlFieldNameWithPrefix, guid);
			if (indexResolverExpression != null)
			{
				stringBuilder.AppendFormat(@"<input type=""hidden"" name=""{0}[{1}].{2}"" value=""{1}"" />", htmlFieldNameWithPrefix, guid, ExpressionHelper.GetExpressionText(indexResolverExpression));
			}
			return new MvcHtmlString(stringBuilder.ToString());
		}

		public static MvcHtmlString EditorForMany<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TValue>>> propertyExpression, Expression<Func<TValue, string>> indexResolverExpression = null, bool includeIndexField = true)
		where TModel : class
		{
			IEnumerable<TValue> tValues = propertyExpression.Compile()(html.ViewData.Model);
			StringBuilder stringBuilder = new StringBuilder();
			string expressionText = ExpressionHelper.GetExpressionText(propertyExpression);
			string fullHtmlFieldName = html.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
			Func<TValue, string> func;
			func = indexResolverExpression?.Compile() ?? (x => null);
			foreach (TValue tValue in tValues)
			{
				stringBuilder.Append("<div class=\"item-render\">");
				var variable = new { Item = tValue };
				string str = func(tValue);
				Expression<Func<TModel, TValue>> expression = Expression.Lambda<Func<TModel, TValue>>(Expression.MakeMemberAccess(Expression.Constant(variable), variable.GetType().GetProperty("Item")), propertyExpression.Parameters);
				str = (!string.IsNullOrEmpty(str) ? html.AttributeEncode(str) : Guid.NewGuid().ToString());
				if (includeIndexField)
				{
					stringBuilder.Append(_EditorForManyIndexField(fullHtmlFieldName, str, indexResolverExpression));
				}
				stringBuilder.Append(html.EditorFor(expression, null, $"{expressionText}[{str}]"));
				stringBuilder.Append("</div>");
			}
			return new MvcHtmlString(stringBuilder.ToString());
		}

		public static MvcHtmlString EditorForManyIndexField<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, string>> indexResolverExpression = null)
		{
			string htmlFieldPrefix = html.ViewData.TemplateInfo.HtmlFieldPrefix;
			int num = htmlFieldPrefix.LastIndexOf('[');
			int num1 = htmlFieldPrefix.IndexOf(']', num + 1);
			if (num == -1 || num1 == -1)
			{
				throw new InvalidOperationException("EditorForManyIndexField called when not in a EditorForMany context");
			}
			string str = htmlFieldPrefix.Substring(0, num);
			string str1 = htmlFieldPrefix.Substring(num + 1, num1 - num - 1);
			return _EditorForManyIndexField(str, str1, indexResolverExpression);
		}
	}
}