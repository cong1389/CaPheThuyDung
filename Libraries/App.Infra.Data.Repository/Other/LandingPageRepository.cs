using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Other;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Other
{
    public class LandingPageRepository : RepositoryBase<LandingPage>, ILandingPageRepository
	{
		public LandingPageRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<LandingPage> GetDefaultOrder(IQueryable<LandingPage> query)
		{
			var landingPages = 
				from p in query
				orderby p.Id
				select p;
			return landingPages;
		}

		public IEnumerable<LandingPage> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<LandingPage> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<LandingPage>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.FullName.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Email.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.PhoneNumber.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.DateOfBith.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}