using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace App.Shout
{
    public class RouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}