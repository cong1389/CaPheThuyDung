using System.Web.Mvc;
using App.Core;
using App.Core.Caching;
using App.Service.Customers;
using App.Service.LocaleStringResource;

namespace App.Service.Common
{
    public interface ICommonServices
    {
        IWorkContext WorkContext
        {
            get;
        }

        ILocaleStringResourceService Localization
        {
            get;
        }

        ICacheManager Cache
        {
            get;
        }

        IWebHelper WebHelper { get; }
    }

    public static class ICommonServicesExtensions
    {
        public static TService Resolve<TService>(this ICommonServices services)
        {
            return DependencyResolver.Current.GetService<TService>();
        }
    }
}
