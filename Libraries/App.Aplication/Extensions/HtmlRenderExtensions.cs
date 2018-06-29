using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace App.Aplication.Extensions
{
    public static class HtmlRenderExtensions
	{
		public static IDisposable Delayed(this HtmlHelper helper, string injectionBlockId = null, string isOnlyOne = null)
		{
			return new DelayedInjectionBlock(helper, injectionBlockId, isOnlyOne);
		}

		public static MvcHtmlString RenderDelayed(this HtmlHelper helper, string injectionBlockId = null, bool removeAfterRendering = true)
		{
			Queue<string> queue = DelayedInjectionBlock.GetQueue(helper, injectionBlockId);
			if (!removeAfterRendering)
			{
				return MvcHtmlString.Create(string.Join(Environment.NewLine, queue));
			}

			StringBuilder stringBuilder = new StringBuilder();
			while (queue.Count > 0)
			{
				stringBuilder.AppendLine(queue.Dequeue());
			}

			return MvcHtmlString.Create(stringBuilder.ToString());
		}

	    public static MvcHtmlString DisplayPlaceHolderFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
	    {
	        var result = html.DisplayNameFor(expression).ToHtmlString();

	        return new MvcHtmlString(HttpUtility.HtmlDecode(result));
	    }

        private class DelayedInjectionBlock : IDisposable
		{
			private const string CACHE_KEY = "DCCF8C78-2E36-4567-B0CF-FE052ACCE309";

			private const string UNIQUE_IDENTIFIER_KEY = "DCCF8C78-2E36-4567-B0CF-FE052ACCE309";

			private const string EMPTY_IDENTIFIER = "";

			private readonly HtmlHelper helper;

			private readonly string identifier;

			private readonly string isOnlyOne;

			public DelayedInjectionBlock(HtmlHelper helper, string identifier = null, string isOnlyOne = null)
			{
				this.helper = helper;
				((WebViewPage)this.helper.ViewDataContainer).OutputStack.Push(new StringWriter());
				this.identifier = identifier ?? "";
				this.isOnlyOne = isOnlyOne;
			}

			private static T _GetOrSet<T>(HtmlHelper helper, T defaultValue, string identifier = "")
			where T : class
			{
				object item;
				Dictionary<string, object> storage = GetStorage(helper);
				if (storage.ContainsKey(identifier))
				{
					item = storage[identifier];
				}
				else
				{
					object obj = defaultValue;
					object obj1 = obj;
					storage[identifier] = obj;
					item = obj1;
				}
				return (T)item;
			}

			public void Dispose()
			{
				Stack<TextWriter> outputStack = ((WebViewPage)helper.ViewDataContainer).OutputStack;
				string str = (outputStack.Count == 0 ? string.Empty : outputStack.Pop().ToString());
				Queue<string> queue = GetQueue(helper, identifier);
				Dictionary<string, int> count = _GetOrSet(helper, new Dictionary<string, int>(), "DCCF8C78-2E36-4567-B0CF-FE052ACCE309");
				if (isOnlyOne == null || !count.ContainsKey(isOnlyOne))
				{
					queue.Enqueue(str);
					if (isOnlyOne != null)
					{
						count[isOnlyOne] = queue.Count;
					}
				}
			}

			public static Queue<string> GetQueue(HtmlHelper helper, string identifier = null)
			{
				return _GetOrSet(helper, new Queue<string>(), identifier ?? "");
			}

		    private static Dictionary<string, object> GetStorage(HtmlHelper helper)
			{
				Dictionary<string, object> item = helper.ViewContext.HttpContext.Items["DCCF8C78-2E36-4567-B0CF-FE052ACCE309"] as Dictionary<string, object>;
				if (item == null)
				{
					IDictionary items = helper.ViewContext.HttpContext.Items;
					Dictionary<string, object> strs = new Dictionary<string, object>();
					item = strs;
					items["DCCF8C78-2E36-4567-B0CF-FE052ACCE309"] = strs;
				}
				return item;
			}
		}
	}
}