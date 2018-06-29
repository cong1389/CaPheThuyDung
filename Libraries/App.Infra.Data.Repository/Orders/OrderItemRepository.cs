using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Orders;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Orders
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
	{
		public OrderItemRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public OrderItem GetById(int id)
		{
			var province = FindBy(x => x.Id == id).FirstOrDefault();
			return province;
		}

		protected override IOrderedQueryable<OrderItem> GetDefaultOrder(IQueryable<OrderItem> query)
		{
			var orderItem = 
				from p in query
				orderby p.Id
				select p;
			return orderItem;
		}

		public IEnumerable<OrderItem> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<OrderItem> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<OrderItem>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
                expression = expression.And(x => x.Id.ToString().ToLower().Contains(sortBuider.Keywords.ToLower()));
            }
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}