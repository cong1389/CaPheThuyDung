using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Caching;

namespace App.Core.IO.VirtualPath
{
	public interface IVirtualPathProvider
	{
		bool FileExists(string virtualPath);
		string Combine(params string[] paths);
		string GetFileHash(string virtualPath, IEnumerable<string> dependencies);
		CacheDependency GetCacheDependency(string virtualPath, IEnumerable<string> dependencies, DateTime utcStart);
		Stream OpenFile(string virtualPath);
	}
}