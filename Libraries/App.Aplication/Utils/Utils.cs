using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using App.Core.Extensions;

namespace App.Aplication
{
    public static class Utils
    {
        public static string GetParameter(string param, string defaultValue)
        {
            string stringValue = HttpContext.Current.Request.QueryString[param];
            if (null == stringValue)
            {
                return defaultValue;
            }

            stringValue = stringValue.Contains("?") ? stringValue.Split('?')[0] : stringValue;
            return stringValue;

        }

        public static string CurrentHost => string.Concat("http://", HttpContext.Current.Request.Url.Authority);

        public static string GetBaseUrl => HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        public static bool ExistsCokiee(this HttpCookieCollection cookieCollection, string name)
        {
            if (cookieCollection == null)
            {
                throw new ArgumentNullException("cookieCollection");
            }
            return cookieCollection[name] != null;
        }

        public static bool ExistsFile(this HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }

        public static Guid GetGuid(string value)
        {
            Guid.TryParse(value, out var guid);

            return guid;
        }

        public static long GetTime()
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            TimeSpan universalTime = DateTime.Now.ToUniversalTime() - dateTime;

            return (long)(universalTime.TotalMilliseconds + 0.5);
        }

        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static string NonAccent(this string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                txt = string.Empty;
            }
            else
            {
                string[] strArrays = { "aAeEoOuUiIdDyY", "áàảãạăắằẳẵặâấầẩẫậ", "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "éèẻẽẹêếềểễệ", "ÉÈẺẼẸÊẾỀỂỄỆ", "óòỏõọôốồổỗộơớờởỡợ", "ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ", "úùủũụưứừửữự", "ÚÙỦŨỤƯỨỪỬỮỰ", "íìỉĩị", "ÍÌỈĨỊ", "đ", "Đ", "ýỳỷỹỵ", "ÝỲỶỸỴ" };
                string[] strArrays1 = { "aAeEoOuUiIdDyY", "áàảãạăắằẳẵặâấầẩẫậ", "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "éèẻẽẹêếềểễệ", "ÉÈẺẼẸÊẾỀỂỄỆ", "óòỏõọôốồổỗộơớờởỡợ", "ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ", "úùủũụưứừửữự", "ÚÙỦŨỤƯỨỪỬỮỰ", "íìỉĩị", "ÍÌỈĨỊ", "đ", "Đ", "ýỳỷỹỵ", "ÝỲỶỸỴ" };
                for (int i = 1; i < strArrays.Length; i++)
                {
                    for (int j = 0; j < strArrays[i].Length; j++)
                    {
                        txt = txt.Replace(strArrays1[i][j], strArrays1[0][i - 1]).Replace(strArrays[i][j], strArrays[0][i - 1]);
                    }
                }
                txt = Regex.Replace(Regex.Replace(txt, "[^a-zA-Z0-9_-]", "-", RegexOptions.Compiled), "-+", "-", RegexOptions.Compiled).Trim('-');
            }
            return txt.ToLower();
        }

        public static string TrimVietNamMark(this string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                txt = string.Empty;
            }
            else
            {
                string[] strArrays = { "aAeEoOuUiIdDyY", "áàảãạăắằẳẵặâấầẩẫậ", "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "éèẻẽẹêếềểễệ", "ÉÈẺẼẸÊẾỀỂỄỆ", "óòỏõọôốồổỗộơớờởỡợ", "ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ", "úùủũụưứừửữự", "ÚÙỦŨỤƯỨỪỬỮỰ", "íìỉĩị", "ÍÌỈĨỊ", "đ", "Đ", "ýỳỷỹỵ", "ÝỲỶỸỴ" };
                string[] strArrays1 = { "aAeEoOuUiIdDyY", "áàảãạăắằẳẵặâấầẩẫậ", "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "éèẻẽẹêếềểễệ", "ÉÈẺẼẸÊẾỀỂỄỆ", "óòỏõọôốồổỗộơớờởỡợ", "ÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ", "úùủũụưứừửữự", "ÚÙỦŨỤƯỨỪỬỮỰ", "íìỉĩị", "ÍÌỈĨỊ", "đ", "Đ", "ýỳỷỹỵ", "ÝỲỶỸỴ" };
                for (int i = 1; i < strArrays.Length; i++)
                {
                    for (int j = 0; j < strArrays[i].Length; j++)
                    {
                        txt = txt.Replace(strArrays1[i][j], strArrays1[0][i - 1]).Replace(strArrays[i][j], strArrays[0][i - 1]);
                    }
                }
            }
            return txt.ToLower();
        }

        public static string SplitWords(int length, string words)
        {
            if (string.IsNullOrEmpty(words))
            {
                return string.Empty;
            }
            if (words.Length < length)
            {
                return words;
            }
            return string.Concat(words.Substring(0, length), "...");
        }

        /// <summary>
        /// Format lại tên file, có dạng: fileName-123-456-789.png
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string FileNameFormat(this string fileName, string fileExtension)
        {
            if (fileName.Length > 250)
            {
                fileName = SplitWords(250, fileName);
            }
            var rndNext = Guid.NewGuid();
            var numberTick = rndNext.ToString().Split('-')[0];

            return $"{fileName.NonAccent()}.{numberTick}{fileExtension}";
            //var rnd = new Random(DateTime.Now.Millisecond);

            //lock (rnd) // synchronize
            //{
            //    var rndNext = rnd.Next(1, 9999);
            //    return $"{fileName.NonAccent()}.{rndNext}{fileExtension}";
            //}
        }

        public static string FolderName(string fileName = null)
        {
            string result = $"{DateTime.UtcNow:yyyyMMdd}";

            if (fileName != null)
            {
                var firstCharsa = fileName.Where((ch, index) => ch != ' ' && (index == 0 || fileName[index - 1] == ' '));
                var firstLeter = string.Join("", firstCharsa.ToArray()).NonAccent().ToUpper();

                result = $"{DateTime.UtcNow.Year}.{firstLeter}";
            }

            return result;
        }

        private static string BuildUrlCore(string virtualPath, string query, string host)
        {
            string appPath = "/", _host = string.Empty;
            if (HostingEnvironment.IsHosted)
            {
                appPath = HostingEnvironment.ApplicationVirtualPath.EmptyNull();
                var uri = HttpContext.Current.Request.Url;
                _host = "//{0}{1}".FormatInvariant(uri.Authority, appPath);
            }

            var _appPath = appPath.EnsureEndsWith("/");

            if (host == null)
            {
                host = _host;
            }
            else if (host == string.Empty)
            {
                host = _appPath;
            }
            else
            {
                host = host.EnsureEndsWith("/");
            }

            var sb = new StringBuilder(host, 100);

            // Strip leading "/", the host/apppath has this already
            if (virtualPath[0] == '/')
            {
                virtualPath = virtualPath.Substring(1);
            }

            // Append media path
            sb.Append(virtualPath);

            // Append query
            if (!string.IsNullOrEmpty(query))
            {
                if (query[0] != '?') sb.Append("?");
                sb.Append(query);
            }

            return sb.ToString();
        }

        public static bool IsHomePage()
        {
            var routeData = HttpContext.Current.Request.RequestContext.RouteData;
            return routeData.GetRequiredString("controller").IsCaseInsensitiveEqual("Home") &&
                          routeData.GetRequiredString("action").IsCaseInsensitiveEqual("Index");
        }

        public static string ValidateImage(this string imagePath)
        {
            string src = string.Empty, url = string.Empty;
            var query = "";
            try
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    src = HttpContext.Current.Server.MapPath(String.Concat("~/", imagePath));

                    url = File.Exists(src) ? BuildUrlCore(imagePath, query, null) : BuildUrlCore(Contains.ImageNoExsits, query, null);
                }
                else
                {
                    url = BuildUrlCore(Contains.ImageNoExsits, query, null);
                }
            }
            catch (Exception ex)
            {
                string exx = ex.Message;
            }

            return url;
        }

        public static string FormatPrice(this decimal? amount)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;

            var fmt = cultureInfo.NumberFormat;
            return amount?.ToString("C", fmt).Replace(",00", "");
        }

        public static string FormatPersent(this decimal? amount)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;

            var fmt = cultureInfo.NumberFormat;

            return $"{amount?.ToString("G29")}%";
        }

        #region Url

        public static string CombineUrl(string baseUrl, string relativeUrl)
        {
            UriBuilder baseUri = new UriBuilder(baseUrl);

            if (Uri.TryCreate(baseUri.Uri, relativeUrl, out var newUri))
            {
                return newUri.ToString();
            }

            throw new ArgumentException("Unable to combine specified url values");
        }

        #endregion
    }
}