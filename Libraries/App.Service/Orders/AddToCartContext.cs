using System;
using App.Domain.Entities.Orders;
using Domain.Entities.Customers;

namespace App.Service.Orders
{
    public class AddToCartContext
    {
        public Domain.Entities.Data.Post Post { get; set; }

        public Customer Customers { get; set; }

        public ShoppingCartItem ShoppingCart { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
