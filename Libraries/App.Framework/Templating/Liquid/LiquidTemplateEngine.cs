using App.Core.Common;
using App.Core.Extensions;
using App.Core.IO.VirtualPath;
using App.Core.Templating;
using App.Framework.Templating.Filters;
using DotLiquid;
using DotLiquid.FileSystems;
using DotLiquid.NamingConventions;
using System;
using System.IO;
using System.Web;

namespace App.Framework.Templating.Liquid
{
    public partial class LiquidTemplateEngine :ITemplateEngine, ITemplateFileSystem
    {
        private readonly IVirtualPathProvider _vpp;

        public LiquidTemplateEngine(IVirtualPathProvider vpp)
        {
            _vpp = vpp;

            // Register Filters
            Template.RegisterFilter(typeof(AdditionalFilters));

            Template.NamingConvention = new CSharpNamingConvention();
            Template.FileSystem = this;
        }

        public string Render(string source, object model, IFormatProvider formatProvider)
        {
            return Compile(source).Render(model, formatProvider);
        }

        public ITemplate Compile(string source)
        {
            return new LiquidTemplate(Template.Parse(source), source);
        }

        public Template GetTemplate(Context context, string templateName)
        {
            var virtualPath = ResolveVirtualPath(context, templateName);

            if (virtualPath.IsEmpty())
            {
                return null;
            }

            var cacheKey = HttpRuntime.Cache.BuildScopedKey("LiquidPartial://" + virtualPath);
            var cachedTemplate = HttpRuntime.Cache.Get(cacheKey);

            if (cachedTemplate == null)
            {
                // Read from file, compile and put to cache with file dependeny
                var source = ReadTemplateFileInternal(virtualPath);
                cachedTemplate = Template.Parse(source);
                var cacheDependency = _vpp.GetCacheDependency(virtualPath, DateTime.UtcNow);
                HttpRuntime.Cache.Insert(cacheKey, cachedTemplate, cacheDependency);
            }

            return (Template)cachedTemplate;
        }

        public string ReadTemplateFile(Context context, string templateName)
        {
            var virtualPath = ResolveVirtualPath(context, templateName);

            return ReadTemplateFileInternal(virtualPath);
        }

	    //public ITestModel CreateTestModelFor(BaseEntity entity, string modelPrefix)
	    //{
	    //    return new TestDrop(entity, modelPrefix);
     //   }

		private string ReadTemplateFileInternal(string virtualPath)
        {
            if (virtualPath.IsEmpty())
            {
                return string.Empty;
            }

            if (!_vpp.FileExists(virtualPath))
            {
                throw new FileNotFoundException($"Include file '{virtualPath}' does not exist.");
            }

            using (var stream = _vpp.OpenFile(virtualPath))
            {
                return stream.AsString();
            }
        }

        private string ResolveVirtualPath(Context context, string templateName)
        {
            var path = ((string)context[templateName]).NullEmpty() ?? templateName;

            if (path.IsEmpty())
			{
				return string.Empty;
			}

			path = path.EnsureEndsWith(".liquid");

            string virtualPath;

            if (!path.StartsWith("~/"))
            {
				//var currentTheme = _themeContext.Value.CurrentTheme;
				virtualPath = _vpp.Combine("~/Themes/Basic/Views/Shared/EmailTemplates", path);
			}
            else
            {
                virtualPath = VirtualPathUtility.ToAppRelative(path);
            }
            return virtualPath;
        }

		public ITestModel CreateTestModelFor(BaseEntity entity, string modelPrefix)
		{
			throw new NotImplementedException();
		}

		#region Services

		//public ICommonServices Services => _services.Value;


		#endregion
	}
}
