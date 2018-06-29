using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Resources;

namespace App.Aplication.WebGrid
{
    public static class WebGridHelpers
    {
        //public static HtmlString WebGridFilter<T>(this HtmlHelper helper,
        //    IEnumerable<T> users, Func<T, string> property,
        //    string headingText) where T : class
        //{
        //    var model = new WebGridFilterModel
        //    {
        //        Users = users.GroupBy(property).Select(g => g.First()),
        //        Property = property,
        //        HeadingText = headingText
        //    };
        //    return helper.Partial("_webGridFilter", model);
        //}

        public static MvcHtmlString EditableTextBox(this HtmlHelper helper,string value, string name, string id)
        {
            // Text Display
            var span = new TagBuilder("span") { InnerHtml = value };
            span.AddCssClass("cell-value");

            // Input display.
            var formatName = HtmlHelper.GenerateIdFromName(name);

            var uniqueId = $"{formatName}_{id}";

            var input = helper.TextBox(uniqueId,
                value, new { @class = "hide input-sm form-control" });

            var result = string.Concat(
                span.ToString(TagRenderMode.Normal),
                input.ToHtmlString()
                );

            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString EditableDateTime(this HtmlHelper helper,DateTime value, string name, string id)
        {
            // Text Display
            var span = new TagBuilder("span") { InnerHtml = value.ToString("yyyy-MM-dd") };
            span.AddCssClass("cell-value");

            // Input display.
            var formatName = HtmlHelper.GenerateIdFromName(name);

            var uniqueId = $"{formatName}_{id}";

            var input = helper.TextBox(uniqueId, value.ToString("yyyy-MM-dd"),
                new { type = "date", @class = "hide input-sm" });

            var result = string.Concat(
                span.ToString(TagRenderMode.Normal),
                input.ToHtmlString()
                );
            return MvcHtmlString.Create(result);
        }

		public static MvcHtmlString DisplayRecordOptions(this HtmlHelper helper)
		{
			// Text Display
			var toolbar = new TagBuilder("ul");
			toolbar.AddCssClass("record-toolbar");

			var result = string.Concat(
				GetEditButton().ToString(TagRenderMode.Normal),
				GetSaveButton().ToString(TagRenderMode.Normal),
				GetCancelButton().ToString(TagRenderMode.Normal),
				GetRemoveButton().ToString(TagRenderMode.Normal)
				);

			toolbar.InnerHtml = result;

			return MvcHtmlString.Create(toolbar.ToString(TagRenderMode.Normal));
		}

		private static TagBuilder GetEditButton()
		{
			var editButton = new TagBuilder("li")
			{
				InnerHtml = $"{GetIcon("edit")} {FormUI.Edit}"

			};

			editButton.AddCssClass("edit-button btn btn-default btn-sm");
			editButton.Attributes.Add("title", FormUI.Edit);

			return editButton;
		}

		private static TagBuilder GetCancelButton()
		{
			var cancelButton = new TagBuilder("li")
			{
				InnerHtml = $"{GetIcon("ban")} {FormUI.Cancel}"
			};

			cancelButton.AddCssClass("cancel-button hide btn btn-default btn-sm");
			cancelButton.Attributes.Add("title", FormUI.Cancel);

			return cancelButton;
		}

		private static TagBuilder GetSaveButton()
		{
			var saveButton = new TagBuilder("li")
			{
				InnerHtml = $"{GetIcon("save")} {FormUI.Save}"
			};

			saveButton.AddCssClass("save-button hide btn btn-default btn-sm");
			saveButton.Attributes.Add("title", FormUI.Save);

			return saveButton;
		}

		private static TagBuilder GetRemoveButton()
		{
			var removeButton = new TagBuilder("li")
			{
				InnerHtml = $"{GetIcon("trash-o")} {FormUI.Delete}"
			};

			removeButton.AddCssClass("remove-button btn btn-default btn-sm");
			removeButton.Attributes.Add("title", FormUI.Delete);

			return removeButton;
		}

		private static string GetIcon(string iconName)
		{
			var icon = new TagBuilder("i");
			icon.AddCssClass($"fa fa-{iconName}");

			return icon.ToString(TagRenderMode.Normal);
		}

	}
}
