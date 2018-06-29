using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Manufacturers
{
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
	{
		public ManufacturerRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<Manufacturer> GetDefaultOrder(IQueryable<Manufacturer> query)
		{
			var manufacturers = 
				from p in query
				orderby p.Id
				select p;

			return manufacturers;
		}

		public IEnumerable<Manufacturer> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Manufacturer> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Manufacturer>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Title.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}

		public IEnumerable<Manufacturer> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Manufacturer>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Title.Contains(sortBuider.Keywords) && x.Status == 1);
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}