using App.Domain.Entities.Orders;

namespace App.Service.Post
{
    public class PriceCalculationService : IPriceCalculationService
    {
        public virtual decimal GetSubTotal(ShoppingCartItem shoppingCartItem, bool includeDiscounts) => (decimal) (shoppingCartItem.CustomerEnteredPrice * shoppingCartItem.Quantity);
    }
}
