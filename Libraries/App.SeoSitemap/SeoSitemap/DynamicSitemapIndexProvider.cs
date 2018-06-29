using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.SeoSitemap
{
    public class DynamicSitemapIndexProvider : IDynamicSitemapIndexProvider
	{
		public DynamicSitemapIndexProvider()
		{
		}

		private ActionResult CreateSitemap<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration, List<T> items)
		{
			var sitemapIndexConfiguration1 = sitemapIndexConfiguration;
			var list = items.Select(sitemapIndexConfiguration1.CreateNode).ToList();
			return sitemapProvider.CreateSitemap(new SitemapModel(list)
			{
				StyleSheets = sitemapIndexConfiguration.SitemapStyleSheets
			});
		}

		public ActionResult CreateSitemapIndex<T>(ISitemapProvider sitemapProvider, ISitemapIndexConfiguration<T> sitemapIndexConfiguration)
		{
			if (sitemapProvider == null)
			{
				throw new ArgumentNullException("sitemapProvider");
			}
			if (sitemapIndexConfiguration == null)
			{
				throw new ArgumentNullException("sitemapIndexConfiguration");
			}
			int num = sitemapIndexConfiguration.DataSource.Count();
			if (sitemapIndexConfiguration.Size >= num)
			{
				return CreateSitemap(sitemapProvider, sitemapIndexConfiguration, sitemapIndexConfiguration.DataSource.ToList());
			}
			if (!sitemapIndexConfiguration.CurrentPage.HasValue || sitemapIndexConfiguration.CurrentPage.Value <= 0)
			{
				int num1 = (int)Math.Ceiling(num / (double)sitemapIndexConfiguration.Size);
				return sitemapProvider.CreateSitemapIndex(CreateSitemapIndex(sitemapIndexConfiguration, num1));
			}
			int? currentPage = sitemapIndexConfiguration.CurrentPage;
			int value = (currentPage.Value - 1) * sitemapIndexConfiguration.Size;
			List<T> list = sitemapIndexConfiguration.DataSource.Skip(value).Take(sitemapIndexConfiguration.Size).ToList();
			return CreateSitemap(sitemapProvider, sitemapIndexConfiguration, list);
		}

		private SitemapIndexModel CreateSitemapIndex<T>(ISitemapIndexConfiguration<T> sitemapIndexConfiguration, int sitemapCount)
		{
			IEnumerable<int> nums = Enumerable.Range(1, sitemapCount);
			if (sitemapIndexConfiguration.UseReverseOrderingForSitemapIndexNodes)
			{
				nums = nums.Reverse();
			}
			ISitemapIndexConfiguration<T> sitemapIndexConfiguration1 = sitemapIndexConfiguration;
			return new SitemapIndexModel(nums.Select(sitemapIndexConfiguration1.CreateSitemapIndexNode).ToList())
			{
				StyleSheets = sitemapIndexConfiguration.SitemapIndexStyleSheets
			};
		}
	}
}