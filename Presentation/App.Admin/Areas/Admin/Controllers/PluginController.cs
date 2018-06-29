using System;
using System.Collections.Generic;
using App.Admin.Areas.Admin.Extensions;
using App.Admin.Areas.Model.Plugins;
using App.Core.Extensions;
using App.Core.Plugins;
using App.FakeEntity.Plugins;
using App.Service.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Admin.Controllers
{
	public class PluginController : BaseAdminController
	{
		private readonly IPluginFinder _pluginFinder;
		private readonly ICommonServices _services;

		public PluginController(IPluginFinder pluginFinder, ICommonServices services)
		{
			_pluginFinder = pluginFinder;
			_services = services;
		}

		// GET: Admin/Plugin
		public ActionResult Index()
		{
			var model = PrepareLocalPluginsModel();

			return View(model);
		}

		[NonAction]
		protected LocalPluginsModel PrepareLocalPluginsModel()
		{
			var plugins = _pluginFinder.GetPluginDescriptors(false)
				.OrderBy(p => p.Group, PluginFileParser.KnownGroupComparer)
				.ThenBy(p => p.DisplayOrder)
				.Select(x => PreparePluginModel(x));

			var model = new LocalPluginsModel();

			var groupedPlugins = from p in plugins
								 group p by p.Group into g
								 select g;

			foreach (var group in groupedPlugins)
			{
				foreach (var plugin in group)
				{
					model.Groups.Add(group.Key, plugin);
				}
			}

			return model;
		}


		[NonAction]
		protected PluginModel PreparePluginModel(PluginDescriptor pluginDescriptor, bool forList = true)
		{
			var model = pluginDescriptor.ToModel();

			// Using GetResource because T could fallback to NullLocalizer here.
			model.Group = _services.Localization.GetResource("Admin.Plugins.KnownGroup." + pluginDescriptor.Group);

			if (forList)
			{
				model.FriendlyName = pluginDescriptor.FriendlyName;
				model.Description = pluginDescriptor.Description;
			}

			// Icon
			//model.IconUrl = _pluginMediator.GetIconUrl(pluginDescriptor);

			if (pluginDescriptor.Installed)
			{
				// specify configuration URL only when a plugin is already installed
				if (pluginDescriptor.IsConfigurable)
				{
					//model.ConfigurationUrl = Url.Action("ConfigurePlugin", new { systemName = pluginDescriptor.SystemName });

					if (!forList)
					{
						var configurable = pluginDescriptor.Instance() as IConfigurable;

						string actionName;
						string controllerName;
						RouteValueDictionary routeValues;
						configurable.GetConfigurationRoute(out actionName, out controllerName, out routeValues);

						if (actionName.HasValue() && controllerName.HasValue())
						{
							//model.ConfigurationRoute = new RouteInfo(actionName, controllerName, routeValues);
						}
					}
				}
			}

			return model;
		}

	    [HttpPost]
        public JsonResult ExecuteTask(string pluginToInstall, string pluginsToUninstall)
		{
			try
			{
			    var tasksCount = 0;
                IEnumerable<PluginDescriptor> descriptors;

                // Uninstall first
			    if (!string.IsNullOrEmpty(pluginsToUninstall))
                {
			        descriptors = _pluginFinder.GetPluginDescriptors(false).Where(x => pluginsToUninstall.Contains(x.SystemName));
			        foreach (var d in descriptors)
			        {
			            if (d.Installed)
			            {
			                d.Instance().Uninstall();
			                tasksCount++;
			            }
			        }
			    }

                if (!string.IsNullOrEmpty(pluginToInstall))
				{
					descriptors = _pluginFinder.GetPluginDescriptors(false).Where(x => pluginToInstall.Contains(x.SystemName));
					foreach (var d in descriptors)
					{
						if (!d.Installed)
						{
							d.Instance().Install();
						    tasksCount++;
                        }
					}
				}

			    if (tasksCount >0)
			    {
			        _services.WebHelper.RestartAppDomain(aggressive: true);
                }
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		    var jsonResult = Json(new { success = true, list = ""},
		        JsonRequestBehavior.AllowGet);

		    return jsonResult;
        }
	}
}