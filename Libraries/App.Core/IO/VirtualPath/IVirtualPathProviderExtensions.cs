using System;
using System.Web.Caching;

namespace App.Core.IO.VirtualPath
{
	public static class VirtualPathProviderExtensions
	{
		public static string GetFileHash(this IVirtualPathProvider vpp, string virtualPath)
		{
			return vpp.GetFileHash(virtualPath, new[] { virtualPath });
		}

		public static CacheDependency GetCacheDependency(this IVirtualPathProvider vpp, string virtualPath, DateTime utcStart)
		{
			return vpp.GetCacheDependency(virtualPath, new[] { virtualPath }, utcStart);
		}
	}
}