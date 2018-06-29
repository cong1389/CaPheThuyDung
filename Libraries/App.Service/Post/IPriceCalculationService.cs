using App.Domain.Entities.Orders;

namespace App.Service.Post
{
    public interface IPriceCalculationService
    {
        decimal GetSubTotal(ShoppingCartItem shoppingCartItem, bool includeDiscounts);
    }
}
