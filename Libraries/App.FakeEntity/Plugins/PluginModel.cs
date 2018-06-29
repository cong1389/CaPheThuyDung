using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using App.Service.Language;

namespace App.FakeEntity.Plugins
{
    public class PluginModel// : ModelBase
    {
        public PluginModel()
        {
        }

        [AllowHtml]
        public string Group { get; set; }

        [AllowHtml]
        public string FriendlyName { get; set; }

        [AllowHtml]
        public string SystemName { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string Version { get; set; }

        [AllowHtml]
        public string Author { get; set; }

        public int DisplayOrder { get; set; }

        public string ConfigurationUrl { get; set; }

        public string Url { get; set; }

        public bool Installed { get; set; }
		
        public bool IsConfigurable { get; set; }
		
        public string IconUrl { get; set; }

        public int[] SelectedStoreIds { get; set; }
    }
}