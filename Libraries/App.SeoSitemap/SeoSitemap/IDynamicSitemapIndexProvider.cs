using System.Web.Mvc;

namespace App.SeoSitemap
{
	public interface IDynamicSitemapIndexProvider
	{
		ActionResult CreateSitemapIndex<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration);
	}
}