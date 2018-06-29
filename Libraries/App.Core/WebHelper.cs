using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using App.Core.Data;
using App.Core.Extensions;
using App.Core.Infrastructure;
using App.Core.Utilities;

namespace App.Core
{
    public partial class WebHelper : IWebHelper
    {
        private static bool? _sOptimizedCompilationsEnabled;
        private static AspNetHostingPermissionLevel? _sTrustLevel;
        private static readonly Regex SStaticExts = new Regex(@"(.*?)\.(css|js|png|jpg|jpeg|gif|webp|scss|less|liquid|bmp|html|htm|xml|pdf|doc|xls|rar|zip|7z|ico|eot|svg|ttf|woff|otf|axd|ashx)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex SHtmlPathPattern = new Regex(@"(?<=(?:href|src)=(?:""|'))(?!https?://)(?<url>[^(?:""|')]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex SCssPathPattern = new Regex(@"url\('(?<url>.+)'\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly ConcurrentDictionary<int, string> _sSafeLocalHostNames = new ConcurrentDictionary<int, string>();

        private readonly HttpContextBase _httpContext;
        private string _ipAddress;

        public WebHelper()
        {
            _httpContext = new HttpContextWrapper(HttpContext.Current);
        }

        public virtual string GetUrlReferrer()
        {
            string referrerUrl = null;

            if (_httpContext?.Request != null && _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.ToString();

            return referrerUrl.EmptyNull();
        }

        public virtual string GetClientIdent()
        {
            var ipAddress = this.GetCurrentIpAddress();
            var userAgent = _httpContext.Request?.UserAgent.EmptyNull();

            if (ipAddress.HasValue() && userAgent.HasValue())
            {
                return (ipAddress + userAgent).GetHashCode().ToString();
            }

            return null;
        }

        public virtual string GetCurrentIpAddress()
        {
            if (_ipAddress != null)
            {
                return _ipAddress;
            }

            if (_httpContext == null && _httpContext.Request == null)
            {
                return string.Empty;
            }

            var vars = _httpContext.Request.ServerVariables;

            var keysToCheck = new string[]
            {
                "HTTP_CLIENT_IP",
                "HTTP_X_FORWARDED_FOR",
                "HTTP_X_FORWARDED",
                "HTTP_X_CLUSTER_CLIENT_IP",
                "HTTP_FORWARDED_FOR",
                "HTTP_FORWARDED",
                "REMOTE_ADDR",
                "HTTP_CF_CONNECTING_IP"
            };

            string result = null;

            foreach (var key in keysToCheck)
            {
                var ipString = vars[key];
                if (ipString.HasValue())
                {
                    var arrStrings = ipString.Split(',');
                    // Take the last entry
                    ipString = arrStrings[arrStrings.Length - 1].Trim();

                    IPAddress address;
                    if (IPAddress.TryParse(ipString, out address))
                    {
                        result = ipString;
                        break;
                    }
                }
            }

            if (result == "::1")
            {
                result = "127.0.0.1";
            }

            return _ipAddress = result.EmptyNull();
        }

        public virtual string ServerVariables(string name)
        {
            string result = string.Empty;

            try
            {
                if (_httpContext?.Request.ServerVariables[name] != null)
                {
                    result = _httpContext.Request.ServerVariables[name];
                }
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private string GetHostPart(string url)
        {
            var uri = new Uri(url);
            var host = uri.GetComponents(UriComponents.Scheme | UriComponents.Host, UriFormat.Unescaped);
            return host;
        }

        public virtual bool IsStaticResource(HttpRequest request)
        {
            return IsStaticResourceRequested(new HttpRequestWrapper(request));
        }

        public static bool IsStaticResourceRequested(HttpRequest request)
        {
            return SStaticExts.IsMatch(request.Path);
        }

        private static bool IsStaticResourceRequested(HttpRequestBase request)
        {
            return SStaticExts.IsMatch(request.Path);
        }

        protected virtual string MapPath(string path)
        {
            return CommonHelper.MapPath(path, false);
        }

        public virtual string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
            url = url.EmptyNull();
            queryStringModification = queryStringModification.EmptyNull();

            string curAnchor = null;

            var hsIndex = url.LastIndexOf('#');
            if (hsIndex >= 0)
            {
                curAnchor = url.Substring(hsIndex);
                url = url.Substring(0, hsIndex);
            }

            var parts = url.Split(new[] { '?' });
            var current = new QueryString(parts.Length == 2 ? parts[1] : "");
            var modify = new QueryString(queryStringModification);

            foreach (var nv in modify.AllKeys)
            {
                current.Add(nv, modify[nv], true);
            }

            var result = string.Concat(
                parts[0],
                current.ToString(false),
                anchor.NullEmpty() == null ? (curAnchor == null ? "" : "#" + curAnchor) : "#" + anchor
            );

            return result;
        }

        public virtual string RemoveQueryString(string url, string queryString)
        {
            var parts = url.SplitSafe("?");

            var current = new QueryString(parts.Length == 2 ? parts[1] : "");

            if (current.Count > 0 && queryString.HasValue())
            {
                current.Remove(queryString);
            }

            var result = string.Concat(parts[0], current.ToString(false));
            return result;
        }

        public virtual T QueryString<T>(string name)
        {
            string queryParam = null;

            if (_httpContext != null && _httpContext.Request.QueryString[name] != null)
                queryParam = _httpContext.Request.QueryString[name];

            if (!String.IsNullOrEmpty(queryParam))
                return queryParam.Convert<T>();

            return default(T);
        }

        private void DeleteMvcTypeCacheFiles()
        {
            try
            {
                var userCacheDir = Path.Combine(HttpRuntime.CodegenDir, "UserCache");

                File.Delete(Path.Combine(userCacheDir, "MVC-ControllerTypeCache.xml"));
                File.Delete(Path.Combine(userCacheDir, "MVC-AreaRegistrationTypeCache.xml"));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private bool TryWriteBinFolder()
        {
            try
            {
                var binMarker = MapPath("~/bin/HostRestart");
                Directory.CreateDirectory(binMarker);

                using (var stream = File.CreateText(Path.Combine(binMarker, "marker.txt")))
                {
                    stream.WriteLine("Restart on '{0}'", DateTime.UtcNow);
                    stream.Flush();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static bool OptimizedCompilationsEnabled
        {
            get
            {
                if (!_sOptimizedCompilationsEnabled.HasValue)
                {
                    var section = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
                    _sOptimizedCompilationsEnabled = section.OptimizeCompilations;
                }

                return _sOptimizedCompilationsEnabled.Value;
            }
        }

        /// <summary>
        /// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_sTrustLevel.HasValue)
            {
                // set minimum
                _sTrustLevel = AspNetHostingPermissionLevel.None;

                // determine maximum
                foreach (AspNetHostingPermissionLevel trustLevel in
                        new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _sTrustLevel = trustLevel;
                        break; //we've set the highest permission we can
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _sTrustLevel.Value;
        }

        /// <summary>
        /// Prepends protocol and host to all (relative) urls in a html string
        /// </summary>
        /// <param name="html">The html string</param>
        /// <param name="request">Request object</param>
        /// <returns>The transformed result html</returns>
        /// <remarks>
        /// All html attributed named <c>src</c> and <c>href</c> are affected, also occurences of <c>url('path')</c> within embedded stylesheets.
        /// </remarks>
        public static string MakeAllUrlsAbsolute(string html, HttpRequestBase request)
        {
            if (request.Url == null)
            {
                return html;
            }

            return MakeAllUrlsAbsolute(html, request.Url.Scheme, request.Url.Authority);
        }

        /// <summary>
        /// Prepends protocol and host to all (relative) urls in a html string
        /// </summary>
        /// <param name="html">The html string</param>
        /// <param name="protocol">The protocol to prepend, e.g. <c>http</c></param>
        /// <param name="host">The host name to prepend, e.g. <c>www.mysite.com</c></param>
        /// <returns>The transformed result html</returns>
        /// <remarks>
        /// All html attributed named <c>src</c> and <c>href</c> are affected, also occurences of <c>url('path')</c> within embedded stylesheets.
        /// </remarks>
        public static string MakeAllUrlsAbsolute(string html, string protocol, string host)
        {
            string baseUrl = $"{protocol}://{host.TrimEnd('/')}";

            MatchEvaluator evaluator = (match) =>
            {
                var url = match.Groups["url"].Value;
                return "{0}{1}".FormatCurrent(baseUrl, url.EnsureStartsWith("/"));
            };

            html = SHtmlPathPattern.Replace(html, evaluator);
            html = SCssPathPattern.Replace(html, evaluator);

            return html;
        }

        /// <summary>
        /// Prepends protocol and host to the given (relative) url
        /// </summary>
        [SuppressMessage("ReSharper", "AccessToModifiedClosure")]
        public static string GetAbsoluteUrl(string url, HttpRequestBase request)
        {
            if (request.Url == null)
            {
                return url;
            }

            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            if (url.StartsWith("~"))
            {
                url = VirtualPathUtility.ToAbsolute(url);
            }

            url = $"{request.Url.Scheme}://{request.Url.Authority}{url}";
            return url;
        }

        public static string GetPublicIPAddress()
        {
            string result = string.Empty;

            try
            {
                using (var client = new WebClient())
                {
                    client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                    try
                    {
                        byte[] arr = client.DownloadData("http://checkip.amazonaws.com/");
                        string response = Encoding.UTF8.GetString(arr);
                        result = response.Trim();
                    }
                    catch { }
                }
            }
            catch { }

            var checkers = new string[]
            {
                "https://ipinfo.io/ip",
                "https://api.ipify.org",
                "https://icanhazip.com",
                "https://wtfismyip.com/text",
                "http://bot.whatismyipaddress.com/"
            };

            if (string.IsNullOrEmpty(result))
            {
                using (var client = new WebClient())
                {
                    foreach (var checker in checkers)
                    {
                        try
                        {
                            result = client.DownloadString(checker).Replace("\n", "");
                            if (!string.IsNullOrEmpty(result))
                            {
                                break;
                            }
                        }
                        catch { }
                    }
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                try
                {
                    var url = "http://checkip.dyndns.org";
                    var req = WebRequest.Create(url);
                    using (var resp = req.GetResponse())
                    {
                        using (var sr = new StreamReader(resp.GetResponseStream()))
                        {
                            var response = sr.ReadToEnd().Trim();
                            var a = response.Split(':');
                            var a2 = a[1].Substring(1);
                            var a3 = a2.Split('<');
                            result = a3[0];
                        }
                    }
                }
                catch { }
            }

            return result;
        }

        public static HttpWebRequest CreateHttpRequestForSafeLocalCall(Uri requestUri)
        {
            var safeHostName = GetSafeLocalHostName(requestUri);

            var uri = requestUri;

            if (!requestUri.Host.Equals(safeHostName, StringComparison.OrdinalIgnoreCase))
            {
                var url =
                    $"{requestUri.Scheme}://{(requestUri.IsDefaultPort ? safeHostName : safeHostName + ":" + requestUri.Port)}{requestUri.PathAndQuery}";
                uri = new Uri(url);
            }

            var request = WebRequest.CreateHttp(uri);
            request.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
            request.ServicePoint.Expect100Continue = false;
            request.UserAgent = "SmartStore.NET {0}".FormatInvariant(SmartStoreVersion.CurrentFullVersion);

            return request;
        }

        private static string GetSafeLocalHostName(Uri requestUri)
        {
            return _sSafeLocalHostNames.GetOrAdd(requestUri.Port, (port) =>
            {
                // first try original host
                if (TestHost(requestUri, requestUri.Host, 5000))
                {
                    return requestUri.Host;
                }

                // try loopback
                var hostName = Dns.GetHostName();
                var hosts = new List<string> { "localhost", hostName, "127.0.0.1" };
                foreach (var host in hosts)
                {
                    if (TestHost(requestUri, host, 500))
                    {
                        return host;
                    }
                }

                // try local IP addresses
                hosts.Clear();
                var ipAddresses = Dns.GetHostAddresses(hostName).Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString());
                hosts.AddRange(ipAddresses);

                foreach (var host in hosts)
                {
                    if (TestHost(requestUri, host, 500))
                    {
                        return host;
                    }
                }

                // None of the hosts are callable. WTF?
                return requestUri.Host;
            });
        }

        private static bool TestHost(Uri originalUri, string host, int timeout)
        {
            var url =
                $"{originalUri.Scheme}://{(originalUri.IsDefaultPort ? host : host + ":" + originalUri.Port)}/taskscheduler/noop";
            var uri = new Uri(url);

            var request = WebRequest.CreateHttp(uri);
            request.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;
            request.ServicePoint.Expect100Continue = false;
            request.UserAgent = "SmartStore.NET";
            request.Timeout = timeout;

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch
            {
                // try the next host
            }
            finally
            {
                response?.Dispose();
            }

            return false;
        }

        public virtual void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "", bool aggressive = false)
        {
            HttpRuntime.UnloadAppDomain();

            if (aggressive)
            {
                // When plugins are (un)installed, 'aggressive' is always true.
                if (OptimizedCompilationsEnabled)
                {
                    // Very hackish:
                    // If optimizedCompilations is on per web.config, touching top-level resources
                    // like global.asax or bin folder is meaningless, 'cause ASP.NET skips these for
                    // hash calculation. This way we can throw in plugins like crazy without invalidating
                    // ASP.NET temp files, which boosts app startup performance dramatically.
                    // Unfortunately, MVC keeps a controller cache file in the temp files folder, which NEVER
                    // gets nuked, unless the 'compilation' element in web.config is changed.
                    // We MUST delete this file in order to ensure that it gets re-created with our new controller types in it.
                    DeleteMvcTypeCacheFiles();
                }
                else
                {
                    // Without optimizedCompilations, touching anything in the bin folder nukes ASP.NET temp folder completely,
                    // including compiled views, MVC cache files etc.
                    TryWriteBinFolder();
                }
            }
            else
            {
                // without this, MVC may fail resolving controllers for newly installed plugins after IIS restart
                Thread.Sleep(250);
            }
        }
    }
}
