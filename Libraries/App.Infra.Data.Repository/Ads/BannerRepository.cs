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
    public class BannerRepository : RepositoryBase<Banner>, IBannerRepository
	{
		public BannerRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Banner GetById(int id)
		{
			var banner = FindBy(x => x.Id == id).FirstOrDefault();
			return banner;
		}

		protected override IOrderedQueryable<Banner> GetDefaultOrder(IQueryable<Banner> query)
		{
			var banners = 
				from p in query
				orderby p.Id
				select p;
			return banners;
		}

		public IEnumerable<Banner> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Banner> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Banner>();
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}