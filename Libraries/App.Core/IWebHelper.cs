using System.Web;

namespace App.Core
{
    public interface IWebHelper
    {
        string GetUrlReferrer();
        string GetClientIdent();
        string GetCurrentIpAddress();
        string ServerVariables(string name);
        bool IsStaticResource(HttpRequest request);
        string ModifyQueryString(string url, string queryStringModification, string anchor);
        string RemoveQueryString(string url, string queryString);
        T QueryString<T>(string name);

        void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "", bool aggressive = false);
    }
}