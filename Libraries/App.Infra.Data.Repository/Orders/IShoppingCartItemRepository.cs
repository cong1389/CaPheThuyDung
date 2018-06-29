using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Orders;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Orders
{
	public interface IShoppingCartItemRepository : IRepositoryBase<ShoppingCartItem>
	{
        ShoppingCartItem GetById(int id);

        IEnumerable<ShoppingCartItem> PagedList(Paging page);

		IEnumerable<ShoppingCartItem> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}