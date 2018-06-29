using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;

namespace App.Core.IO.VirtualPath
{
    public class DefaultVirtualPathProvider : VirtualPathProviderViewEngine, IVirtualPathProvider
	{
	    public DefaultVirtualPathProvider()
	    {
	    }
        
	    public virtual bool FileExists(string virtualPath)
		{
			return HostingEnvironment.VirtualPathProvider.FileExists(virtualPath);
		}

	    public virtual string Combine(params string[] paths)
	    {
	        return Path.Combine(paths).Replace(Path.DirectorySeparatorChar, '/');
	    }

        public virtual string GetFileHash(string virtualPath, IEnumerable<string> dependencies)
	    {
	        return HostingEnvironment.VirtualPathProvider.GetFileHash(virtualPath, dependencies);
	    }

	    public virtual CacheDependency GetCacheDependency(string virtualPath, IEnumerable<string> dependencies, DateTime utcStart)
	    {
	        return HostingEnvironment.VirtualPathProvider.GetCacheDependency(virtualPath, dependencies, utcStart);
	    }

		public virtual Stream OpenFile(string virtualPath)
		{
			return HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).Open();
		}

	    protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
	    {
	        return new RazorView(controllerContext, partialPath, null, false, FileExtensions);
	    }

	    protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
	    {
	        return new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions);
	    }
    }
}
