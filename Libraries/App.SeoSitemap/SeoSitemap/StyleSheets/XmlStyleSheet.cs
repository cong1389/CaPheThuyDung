using App.SeoSitemap.Enum;
using System;
using System.Runtime.CompilerServices;

namespace App.SeoSitemap.StyleSheets
{
	public class XmlStyleSheet
	{
		public YesNo? Alternate
		{
			get;
			set;
		}

		public string Charset
		{
			get;
			set;
		}

		public string Media
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public XmlStyleSheet(string url)
		{
			Url = url;
			Type = "text/xsl";
		}
	}
}