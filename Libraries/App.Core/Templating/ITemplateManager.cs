using System;
using System.Collections.Concurrent;

namespace App.Core.Templating
{
    public interface ITemplateManager
	{
		ConcurrentDictionary<string, ITemplate> All();
		bool Contains(string name);
		ITemplate Get(string name);
		void Put(string name, ITemplate template);
		ITemplate GetOrAdd(string name, Func<string> sourceFactory);
		bool TryRemove(string name, out ITemplate template);
		void Clear();
	}
}