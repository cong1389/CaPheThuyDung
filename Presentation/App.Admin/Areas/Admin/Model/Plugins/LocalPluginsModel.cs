using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Core.Collections;
using App.FakeEntity.Plugins;

namespace App.Admin.Areas.Model.Plugins
{
	public class LocalPluginsModel// : ModelBase
	{

		public LocalPluginsModel()
		{
			this.Groups = new Multimap<string, PluginModel>();
		}

		public Multimap<string, PluginModel> Groups { get; private set; }

		public ICollection<PluginModel> AllPlugins
		{
			get
			{
				return Groups.SelectMany(k => k.Value).ToList().AsReadOnly();
			}
		}

	}
}