using App.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Web;

namespace App.Shout
{
    public class Plugin : BasePlugin
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// /// <param name="settingService">Settings service</param>
        /// /// <param name="settingService">Localization service</param>
        public Plugin()
        {

        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override void Install()
        {
            base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {

            base.Uninstall();
        }
    }
}