using System.Linq;
using App.Domain.Entities.Orders;

namespace App.Service.Orders
{
    public interface IOrderTotalCalculationService
    {
        decimal GetShoppingCartSubTotal(IOrderedEnumerable<ShoppingCartItem> cart);
    }
}
