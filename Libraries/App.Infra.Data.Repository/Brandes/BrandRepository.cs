using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Brandes;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Brandes
{
    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
	{
		public BrandRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Brand GetById(int id)
		{
			var province = FindBy(x => x.Id == id).FirstOrDefault();
			return province;
		}

		protected override IOrderedQueryable<Brand> GetDefaultOrder(IQueryable<Brand> query)
		{
			var brand = 
				from p in query
				orderby p.Id
				select p;
			return brand;
		}

		public IEnumerable<Brand> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Brand> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Brand>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Name.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}