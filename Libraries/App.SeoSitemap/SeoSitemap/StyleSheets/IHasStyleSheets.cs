using System;
using System.Collections.Generic;

namespace App.SeoSitemap.StyleSheets
{
	public interface IHasStyleSheets
	{
		List<XmlStyleSheet> StyleSheets
		{
			get;
			set;
		}
	}
}