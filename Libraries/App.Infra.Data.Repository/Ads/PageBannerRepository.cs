using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Ads
{
    public class PageBannerRepository : RepositoryBase<PageBanner>, IPageBannerRepository
	{
		public PageBannerRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public PageBanner GetById(int id)
		{
			var pageBanner = FindBy(x => x.Id == id).FirstOrDefault();
			return pageBanner;
		}

		protected override IOrderedQueryable<PageBanner> GetDefaultOrder(IQueryable<PageBanner> query)
		{
			var pageBanners = 
				from p in query
				orderby p.Id
				select p;
			return pageBanners;
		}

		public IEnumerable<PageBanner> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<PageBanner> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<PageBanner>();
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}