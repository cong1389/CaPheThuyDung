using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using App.Domain.Orders;

namespace App.Infra.Data.Repository.Orders
{
    public interface IOrderItemRepository : IRepositoryBase<OrderItem>
	{
		OrderItem GetById(int id);

		IEnumerable<OrderItem> PagedList(Paging page);

		IEnumerable<OrderItem> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}