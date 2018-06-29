using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Security;

namespace App.Framework.Utilities
{
    public static class ExtentionUtils
	{
		public static string ApplicationName => Membership.ApplicationName.ToLower();

	    public static int PageSize => int.Parse(ConfigurationManager.AppSettings["ItemsPerPage"]);

	    public static bool Log(string txt)
		{
			bool flag;
			try
			{
				HttpContext current = HttpContext.Current;
				string str = "";
				if (current != null)
				{
					str = current.Request.Url.ToString();
				}
				if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
				{
					Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
				}
				DateTime now = DateTime.Now;
				string str1 = $"Logs/{now:yyyyMMdd}.txt";
				if (!File.Exists(string.Concat(current.Server.MapPath("~/"), str1)))
				{
					File.Create(string.Concat(current.Server.MapPath("~/"), str1));
				}
				FileStream fileStream = File.Open(string.Concat(current.Server.MapPath("~/"), str1), FileMode.Open);
				File.ReadAllLines(string.Concat(current.Server.MapPath("~/"), str1));
				List<string> strs = new List<string>
				{
				    $"{DateTime.Now}\t{txt}\t{str}\r\n"
				};
				File.AppendAllText(string.Concat(current.Server.MapPath("~/"), str1), string.Join(Environment.NewLine, strs));
				fileStream.Close();
				flag = true;
				return flag;
			}
			catch
			{
			}
			flag = false;
			return flag;
		}
		
		public static string ToDisplay(this Enum value)
		{
			string empty;
			if (value != null)
			{
				FieldInfo field = value.GetType().GetField(value.ToString());
				if (field == null)
				{
					empty = string.Empty;
				}
				else
				{
					DisplayAttribute[] customAttributes = (DisplayAttribute[])field.GetCustomAttributes(typeof(DisplayAttribute), false);
					empty = customAttributes.Length != 0 ? customAttributes[0].GetName() : value.ToString();
				}
			}
			else
			{
				empty = "";
			}
			return empty;
		}
	}
}