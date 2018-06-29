using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Extensions;
using App.Core.Localization;
using App.Core.Plugins.Providers;
using App.Service.Common;
using Autofac;

namespace App.Framework.Plugins
{
   public class PluginMediator
	{
		private readonly ICommonServices _services;

		public PluginMediator(ICommonServices services, IComponentContext ctx)
		{
			this._services = services;

			T = NullLocalizer.Instance;
		}

		public Localizer T { get; set; }

	}
}
