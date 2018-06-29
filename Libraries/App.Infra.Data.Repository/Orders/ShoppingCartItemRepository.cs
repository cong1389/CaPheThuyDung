using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Orders;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Orders
{
    public class ShoppingCartItemRepository : RepositoryBase<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ShoppingCartItem GetById(int id)
        {
            var shopCart = FindBy(x => x.Id == id).FirstOrDefault();
            return shopCart;
        }

        protected override IOrderedQueryable<ShoppingCartItem> GetDefaultOrder(IQueryable<ShoppingCartItem> query)
        {
            var orders =
                from p in query
                orderby p.Id
                select p;
            return orders;
        }

        public IEnumerable<ShoppingCartItem> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<ShoppingCartItem> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<ShoppingCartItem>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x =>x.CreatedDate.ToString("dd/MM/yyyy").ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}