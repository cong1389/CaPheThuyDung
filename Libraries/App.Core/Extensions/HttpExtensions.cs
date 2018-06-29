using System.Web.Caching;
using System.Web.Mvc;

namespace App.Core.Extensions
{
    public static class HttpExtensions
    {
        public static ControllerContext GetMasterControllerContext(this ControllerContext controllerContext)
        {
            var ctx = controllerContext;

            while (ctx.ParentActionViewContext != null)
            {
                ctx = ctx.ParentActionViewContext;
            }

            return ctx;
        }

	    public static string BuildScopedKey(this Cache cache, string key)
	    {
		    return key.HasValue() ? "SmartStoreNET:" + key : null;
	    }
	}


}
