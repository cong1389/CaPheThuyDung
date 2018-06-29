using System.Web.Mvc;
using App.Core.Extensions;
using App.Core.Localization;

namespace App.Framework.Theme
{
    public class WebViewPageHelper
    {
        private bool _initialized;

        public WebViewPageHelper()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Initialize(ViewContext viewContext)
        {
            if (_initialized)
            {
                return;
            }

            viewContext.GetMasterControllerContext();
            _initialized = true;
        }
    }
}
