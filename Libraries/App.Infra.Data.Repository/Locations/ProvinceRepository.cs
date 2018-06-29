using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Locations
{
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
	{
		public ProvinceRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Province GetById(int id)
		{
			var province = FindBy(x => x.Id == id).FirstOrDefault();
			return province;
		}

		protected override IOrderedQueryable<Province> GetDefaultOrder(IQueryable<Province> query)
		{
			var provinces = 
				from p in query
				orderby p.Id
				select p;
			return provinces;
		}

		public IEnumerable<Province> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Province> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Province>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Name.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}