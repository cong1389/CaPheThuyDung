using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace App.Framework.Theme
{
    public abstract class ThemeableVirtualPathProviderViewEngine : VirtualPathProviderViewEngine
    {
        private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:";

        private const string CacheKeyPrefixMaster = "Master";

        private const string CacheKeyPrefixPartial = "Partial";

        private const string CacheKeyPrefixView = "View";

        private static readonly string[] EmptyLocations;

        internal Func<string, string> GetExtensionThunk = VirtualPathUtility.GetExtension;

        static ThemeableVirtualPathProviderViewEngine()
        {
            EmptyLocations = new string[0];
        }

        private string AppendDisplayModeToCacheKey(string cacheKey, string displayMode)
        {
            return string.Concat(cacheKey, displayMode, ":");
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string areaName)
        {
            return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:", GetType().AssemblyQualifiedName, prefix, name, controllerName, areaName);
        }

        protected virtual bool FilePathIsSupported(string virtualPath)
        {
            bool flag;
            if (FileExtensions != null)
            {
                var str = GetExtensionThunk(virtualPath).TrimStart('.');
                flag = FileExtensions.Contains(str, StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("Partial view name cannot be null or empty.", "partialViewName");
            }

            var requiredString = controllerContext.RouteData.GetRequiredString("controller");
            var path = GetPath(controllerContext, PartialViewLocationFormats, AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, requiredString, "Partial", useCache, out var strArrays);
            var viewEngineResult = !string.IsNullOrEmpty(path) ? new ViewEngineResult(CreatePartialView(controllerContext, path), this) : new ViewEngineResult(strArrays);

            return viewEngineResult;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            bool flag;
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("View name cannot be null or empty.", "viewName");
            }

            var requiredString = controllerContext.RouteData.GetRequiredString("controller");
            var path = GetPath(controllerContext, ViewLocationFormats, AreaViewLocationFormats, "ViewLocationFormats", viewName, requiredString, "View", useCache, out var strArrays);
            var str = GetPath(controllerContext, MasterLocationFormats, AreaMasterLocationFormats, "MasterLocationFormats", masterName, requiredString, "Master", useCache, out var strArrays1);

            if (string.IsNullOrEmpty(path))
            {
                flag = true;
            }
            else
            {
                flag = string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(masterName);
            }
            var viewEngineResult = !flag ? new ViewEngineResult(CreateView(controllerContext, path, str), this) : new ViewEngineResult(strArrays.Union(strArrays1));

            return viewEngineResult;
        }

        protected virtual string GetAreaName(RouteBase route)
        {
            string item;
            if (!(route is IRouteWithArea routeWithArea))
            {
                if (!(route is Route route1) || route1.DataTokens == null)
                {
                    item = null;
                }
                else
                {
                    item = route1.DataTokens["area"] as string;
                }
            }
            else
            {
                item = routeWithArea.Area;
            }
            return item;
        }

        protected virtual string GetAreaName(RouteData routeData)
        {
            var str = !routeData.DataTokens.TryGetValue("area", out var obj) ? GetAreaName(routeData.Route) : obj as string;
            return str;
        }

        protected virtual string GetPath(ControllerContext controllerContext, string[] locations,
            string[] areaLocations, string locationsPropertyName, string name, string controllerName,
            string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            string empty;
            searchedLocations = EmptyLocations;
            if (!string.IsNullOrEmpty(name))
            {
                var areaName = GetAreaName(controllerContext.RouteData);
                if (!string.IsNullOrEmpty(areaName) && areaName.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
                {
                    var list = areaLocations.ToList();
                    list.Insert(0, "~/Areas/Admin/Views/{1}/{0}.cshtml");
                    list.Insert(0, "~/Areas/Admin/Views/Shared/{0}.cshtml");
                    areaLocations = list.ToArray();
                }
                var flag = !string.IsNullOrEmpty(areaName);
                var strArrays1 = locations;
                string[] strArrays;
                if (flag)
                {
                    strArrays = areaLocations;
                }
                else
                {
                    strArrays = null;
                }
                var viewLocations = GetViewLocations(strArrays1, strArrays);
                if (viewLocations.Count == 0)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Properties cannot be null or empty - {0}", locationsPropertyName));
                }
                var flag1 = IsSpecificPath(name);
                var str = CreateCacheKey(cacheKeyPrefix, name, flag1 ? string.Empty : controllerName, areaName);
                if (!useCache)
                {
                    empty = flag1 ? GetPathFromSpecificName(controllerContext, name, str, ref searchedLocations) : GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName, str, ref searchedLocations);
                }
                else
                {
                    foreach (var availableDisplayModesForContext in DisplayModeProvider.GetAvailableDisplayModesForContext(controllerContext.HttpContext, controllerContext.DisplayMode))
                    {
                        var viewLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(str, availableDisplayModesForContext.DisplayModeId));
                        if (viewLocation == null)
                        {
                        }
                        if (!string.IsNullOrEmpty(viewLocation))
                        {
                            if (controllerContext.DisplayMode == null)
                            {
                                controllerContext.DisplayMode = availableDisplayModesForContext;
                            }
                            empty = viewLocation;
                            return empty;
                        }
                    }
                    empty = null;
                }
            }
            else
            {
                empty = string.Empty;
            }
            return empty;
        }

        protected virtual string GetPathFromGeneralName(ControllerContext controllerContext, List<ViewLocation> locations, string name, string controllerName, string areaName, string cacheKey, ref string[] searchedLocations)
        {
            Func<string, bool> func;
            Func<string, bool> func1 = null;
            Func<string, bool> func2 = null;
            var empty = string.Empty;
            searchedLocations = new string[locations.Count];
            var num = 0;
            while (num < locations.Count)
            {
                var item = locations[num];
                var str = item.Format(name, controllerName, areaName);
                if (!File.Exists(HttpContext.Current.Server.MapPath(str)))
                {
                    str = item.Format(name, controllerName, areaName);
                }
                var displayModeProvider = DisplayModeProvider;
                var str1 = str;
                var httpContext = controllerContext.HttpContext; 
                var func3 = func1;
                if (func3 == null)
                {
                    Func<string, bool> func4 = path => FileExists(controllerContext, path);
                    func = func4;
                    func1 = func4;
                    func3 = func;
                }
                var displayInfoForVirtualPath = displayModeProvider.GetDisplayInfoForVirtualPath(str1, httpContext, func3, controllerContext.DisplayMode);
                if (displayInfoForVirtualPath == null)
                {
                    searchedLocations[num] = str;
                    num++;
                }
                else
                {
                    var filePath = displayInfoForVirtualPath.FilePath;
                    searchedLocations = EmptyLocations;
                    empty = filePath;
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayInfoForVirtualPath.DisplayMode.DisplayModeId), empty);
                    if (controllerContext.DisplayMode == null)
                    {
                        controllerContext.DisplayMode = displayInfoForVirtualPath.DisplayMode;
                    }
                    foreach (var mode in DisplayModeProvider.Modes)
                    {
                        if (mode.DisplayModeId != displayInfoForVirtualPath.DisplayMode.DisplayModeId)
                        {
                            var displayMode = mode;
                            var httpContextBase = controllerContext.HttpContext;
                            var str2 = str;
                            var func5 = func2;
                            if (func5 == null)
                            {
                                Func<string, bool> func6 = path => FileExists(controllerContext, path);
                                func = func6;
                                func2 = func6;
                                func5 = func;
                            }
                            var displayInfo = displayMode.GetDisplayInfo(httpContextBase, str2, func5);
                            var empty1 = string.Empty;
                            if (displayInfo?.FilePath != null)
                            {
                                empty1 = displayInfo.FilePath;
                            }
                            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, mode.DisplayModeId), empty1);
                        }
                    }
                    break;
                }
            }
            return empty;
        }

        protected virtual string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            var empty = name;
            if (!FilePathIsSupported(name) || !FileExists(controllerContext, name))
            {
                empty = string.Empty;
                searchedLocations = new[] { name };
            }
            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, empty);
            return empty;
        }

        protected virtual List<ViewLocation> GetViewLocations(string[] viewLocationFormats, string[] areaViewLocationFormats)
        {
            var viewLocations = new List<ViewLocation>();
            if (areaViewLocationFormats != null)
            {
                var strArrays = areaViewLocationFormats;
                for (var i = 0; i < strArrays.Length; i++)
                {
                    viewLocations.Add(new AreaAwareViewLocation(strArrays[i]));
                }
            }
            if (viewLocationFormats != null)
            {
                var strArrays1 = viewLocationFormats;
                for (var j = 0; j < strArrays1.Length; j++)
                {
                    viewLocations.Add(new ViewLocation(strArrays1[j]));
                }
            }
            return viewLocations;
        }

        protected virtual bool IsSpecificPath(string name)
        {
            var chr = name[0];
            return chr == '~' || chr == '/';
        }

        public class AreaAwareViewLocation : ViewLocation
        {
            public AreaAwareViewLocation(string virtualPathFormatString) : base(virtualPathFormatString)
            {
            }

            public override string Format(string viewName, string controllerName, string areaName)
            {
                return string.Format(CultureInfo.InvariantCulture, VirtualPathFormatString, viewName, controllerName, areaName);
            }
        }

        public class ViewLocation
        {
            protected readonly string VirtualPathFormatString;

            public ViewLocation(string virtualPathFormatString)
            {
                VirtualPathFormatString = virtualPathFormatString;
            }

            public virtual string Format(string viewName, string controllerName, string areaName)
            {
                return string.Format(CultureInfo.InvariantCulture, VirtualPathFormatString, viewName, controllerName);
            }
        }
    }
}