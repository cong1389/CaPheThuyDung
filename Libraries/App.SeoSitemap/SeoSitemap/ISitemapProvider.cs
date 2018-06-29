using System.Web.Mvc;

namespace App.SeoSitemap
{
	public interface ISitemapProvider
	{
		ActionResult CreateSitemap(SitemapModel sitemapModel);

		ActionResult CreateSitemapIndex(SitemapIndexModel sitemapIndexModel);
	}
}