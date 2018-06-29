using System;
using System.Collections.Concurrent;

namespace App.Core.Templating
{
    public partial class DefaultTemplateManager : ITemplateManager
	{
		private readonly ConcurrentDictionary<string, ITemplate> _templates;
		private readonly ITemplateEngine _engine;

		public DefaultTemplateManager(ITemplateEngine engine)
		{
			_templates = new ConcurrentDictionary<string, ITemplate>(StringComparer.OrdinalIgnoreCase);
			_engine = engine;
		}

		public ConcurrentDictionary<string, ITemplate> All()
		{
			return _templates;
		}

		public bool Contains(string name)
		{
			return _templates.ContainsKey(name);
		}

		public ITemplate Get(string name)
		{
			_templates.TryGetValue(name, out var template);
			return template;
		}

		public void Put(string name, ITemplate template)
		{
			_templates[name] = template;
		}

		public ITemplate GetOrAdd(string name, Func<string> sourceFactory)
		{
			return _templates.GetOrAdd(name, key => _engine.Compile(sourceFactory()));
		}

		public bool TryRemove(string name, out ITemplate template)
		{
			return _templates.TryRemove(name, out template);
		}

		public void Clear()
		{
			_templates.Clear();
		}

	}
}
