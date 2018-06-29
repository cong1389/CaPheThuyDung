using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using App.Aplication.PagedSort.SortUtils;

namespace App.Aplication.Extensions
{
    public static class HtmlHelperExtensions
	{
		public static MvcHtmlString CheckBoxListFor<TModel>(this HtmlHelper<TModel> htmlHelper,
		    Expression<Func<TModel, object>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null)
		{
			TagBuilder tagBuilder = new TagBuilder("ul");

			if (htmlAttributes != null)
			{
				tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
			}
			tagBuilder.AddCssClass("check-box-list");
			var modelMetadatum = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

			if (selectList == null)
			{
				return MvcHtmlString.Create(tagBuilder.ToString());
			}

			foreach (SelectListItem selectListItem in selectList)
			{
				TagBuilder li = new TagBuilder("li");
				TagBuilder tagBuilderLable = new TagBuilder("label");
				TagBuilder tagBuilderInput = new TagBuilder("input");

				li.AddCssClass("checkbox");
				tagBuilderInput.MergeAttribute("type", "checkbox");
				tagBuilderInput.MergeAttribute("name", modelMetadatum.PropertyName);
				tagBuilderInput.MergeAttribute("value", selectListItem.Value);

				if (selectListItem.Selected)
				{
					tagBuilderInput.MergeAttribute("checked", "checked");
				}

				tagBuilderInput.GenerateId(modelMetadatum.PropertyName);
				tagBuilderLable.InnerHtml = tagBuilderInput.ToString(TagRenderMode.SelfClosing);

				TagBuilder tagBuilder3 = tagBuilderLable;
				tagBuilder3.InnerHtml = string.Concat(tagBuilder3.InnerHtml, " ", selectListItem.Text);

				li.InnerHtml = tagBuilderLable.ToString();
				TagBuilder tagBuilder4 = tagBuilder;
				tagBuilder4.InnerHtml = string.Concat(tagBuilder4.InnerHtml, li.ToString());
			}

			return MvcHtmlString.Create(tagBuilder.ToString());
		}

		public static MvcHtmlString SortExpressionLink(this HtmlHelper helper, string title, string sortExpression, SortDirection direction, object htmlAttributes = null)
		{
			TagBuilder tagBuilder = new TagBuilder("a");
			if (htmlAttributes != null)
			{
				tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
			}

			TagBuilder tagI = new TagBuilder("i");
			tagI.MergeAttribute("class", "indicator");
			tagBuilder.AddCssClass("sort-expression-link");
			tagBuilder.MergeAttribute("title", title);
			tagBuilder.MergeAttribute("href", string.Concat("#", sortExpression));
			tagBuilder.MergeAttribute("data-sort-expression", sortExpression);
			tagBuilder.MergeAttribute("data-sort-direction", direction.ToString());
			tagBuilder.InnerHtml = string.Concat(title, tagI.ToString(TagRenderMode.Normal));

			return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
		}
	}
}