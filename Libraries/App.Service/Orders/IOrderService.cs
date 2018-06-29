using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;
using App.Domain.Orders;

namespace App.Service.Orders
{
    public interface IOrderService : IBaseService<Order>
	{
		Order GetById(int id, bool isCache = true);

        IEnumerable<Order> GetByCustomerId(int customerId, bool isCache = true);

        IEnumerable<Order> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}