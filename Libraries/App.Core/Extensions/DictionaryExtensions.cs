using System.Collections.Generic;

namespace App.Core.Extensions
{
    public static class DictionaryExtensions
	{
	    public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> instance, TKey key)
	    {
	        instance.TryGetValue(key, out var val);
	        return val;
	    }

	    public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> instance, IDictionary<TKey, TValue> from, bool replaceExisting = true)
	    {
	        foreach (var kvp in from)
	        {
	            if (replaceExisting || !instance.ContainsKey(kvp.Key))
	            {
	                instance[kvp.Key] = kvp.Value;
	            }
	        }
	    }
    }
}
