using System.Linq;
using App.Domain.Entities.Orders;
using Domain.Entities.Customers;

namespace App.Service.Customers
{
    public static class CustomerExtensions
    {
        public static IOrderedEnumerable<ShoppingCartItem> GetCartItems(this Customer customer)
        {
            var items = customer.ShoppingCartItems.OrderByDescending(x => x.Id);
            return items;
        }

        //public static string GetFullName(this Customer customer)
        //{
        //    if (customer == null)
        //        throw new ArgumentNullException("customer null");

        //    string firstName = customer.
        //}

    }
}
