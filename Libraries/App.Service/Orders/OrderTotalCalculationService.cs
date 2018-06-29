using System.Collections.Generic;
using System.Linq;
using App.Domain.Entities.Orders;

namespace App.Service.Orders
{
    public class OrderTotalCalculationService : IOrderTotalCalculationService
    {
        public decimal GetShoppingCartSubTotal(IOrderedEnumerable<ShoppingCartItem> cart)
        {
            var subTotal = decimal.Zero;

            if (!cart.Any())
            {
                return subTotal;
            }

            subTotal = (decimal) (from mul in cart
                select mul.Quantity * mul.CustomerEnteredPrice).Sum();

            return subTotal;
        }

        //public decimal GetShoppingCartTotal(IOrderedEnumerable<ShoppingCartItem> cart)
        //{
        //    var subtotalBase = GetShoppingCartSubTotal(cart);

        //    // Shipping without tax
        //    decimal? shoppingCartShipping = GetShoppingCartShippingTotal(cart, false);
        //}

        //public virtual decimal? GetShoppingCartShippingTotal(IList<ShoppingCartItem> cart, bool includingTax)
        //{
        //    var taxRate = decimal.Zero;
        //    Discount appliedDiscount = null;

        //    return GetShoppingCartShippingTotal(cart, includingTax, out taxRate, out appliedDiscount);
        //}
    }
}
