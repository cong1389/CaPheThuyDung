using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using App.Domain.Orders;

namespace App.Infra.Data.Repository.Orderes
{
    public interface IOrderRepository : IRepositoryBase<Order>
	{
		Order GetById(int id);

        IEnumerable<Order> GetByCustomerId(int customerId);

        IEnumerable<Order> PagedList(Paging page);

		IEnumerable<Order> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}