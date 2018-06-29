using System.Collections.Generic;
using App.Core.ComponentModel;

namespace App.Service.Messages
{
    public class TemplateModel : HybridExpando
	{
		public T GetFromBag<T>(string key)
		{
			if (Contains("Bag") && base["Bag"] is IDictionary<string, object> bag)
			{
				if (bag.TryGetValue(key, out var value) && value is T result)
				{
					return result;
				}
			}

			return default(T);
		}
	}
}
