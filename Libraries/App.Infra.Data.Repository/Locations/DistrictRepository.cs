using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Locations
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
	{
		public DistrictRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public District GetById(int id)
		{
			var district = FindBy(x => x.Id == id).FirstOrDefault();
			return district;
		}

		protected override IOrderedQueryable<District> GetDefaultOrder(IQueryable<District> query)
		{
			var districts = 
				from p in query
				orderby p.Id
				select p;
			return districts;
		}

		public IEnumerable<District> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<District> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<District>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Name.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}