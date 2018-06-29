using System.Text;
using System.Web.Mvc;

namespace App.Framework.Utilities
{
    public static class LayoutExtensions
    {
        public static string FakePathAdmin(this UrlHelper url, string contentPath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("/Areas/Admin/{0}", contentPath);

            //string str = string.Format("/Areas/Admin/{0}", contentPath);
            return url.Content(string.Concat("~/", sb.ToString()));
        }
    }
}