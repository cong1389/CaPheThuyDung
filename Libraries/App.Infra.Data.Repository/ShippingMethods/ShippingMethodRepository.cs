using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Shippings;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.ShippingMethods
{
    public class ShippingMethodRepository : RepositoryBase<ShippingMethod>, IShippingMethodRepository
	{
		public ShippingMethodRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public ShippingMethod GetById(int id)
		{
			var province = FindBy(x => x.Id == id).FirstOrDefault();
			return province;
		}

		protected override IOrderedQueryable<ShippingMethod> GetDefaultOrder(IQueryable<ShippingMethod> query)
		{
			var shippingMethod = 
				from p in query
				orderby p.Id
				select p;
			return shippingMethod;
		}

		public IEnumerable<ShippingMethod> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<ShippingMethod> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<ShippingMethod>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Name.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}