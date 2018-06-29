using System.Collections.Generic;
using System.Linq;
using App.SeoSitemap.StyleSheets;

namespace App.SeoSitemap
{
    public interface ISitemapIndexConfiguration<T>
	{
		int? CurrentPage
		{
			get;
		}

		IQueryable<T> DataSource
		{
			get;
		}

		List<XmlStyleSheet> SitemapIndexStyleSheets
		{
			get;
		}

		List<XmlStyleSheet> SitemapStyleSheets
		{
			get;
		}

		int Size
		{
			get;
		}

		bool UseReverseOrderingForSitemapIndexNodes
		{
			get;
		}

		SitemapNode CreateNode(T source);

		SitemapIndexNode CreateSitemapIndexNode(int currentPage);
	}
}