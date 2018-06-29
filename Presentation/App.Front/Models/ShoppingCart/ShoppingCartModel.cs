using System.Collections.Generic;
using App.Domain.Entities.Data;
using App.Domain.Entities.Orders;
using App.FakeEntity.Common;

namespace App.Front.Models.ShoppingCart
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel()
        {
            Items = new List<Post>();
            ShoppingCarts = new List<ShoppingCartItem>();
            OrderReviewData = new OrderReviewDataModel();
        }

        public IEnumerable<Post> Items { get; set; }

        public IEnumerable<ShoppingCartItem> ShoppingCarts { get; set; }

        public OrderReviewDataModel OrderReviewData { get; set; }
    }

    public class OrderReviewDataModel 
    {
        public OrderReviewDataModel()
        {
            BillingAddress = new AddressViewModel();
            ShippingAddress = new AddressViewModel();
        }
        public bool Display { get; set; }        

        public AddressViewModel BillingAddress { get; set; }

        public bool IsShippable { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public string ShippingMethod { get; set; }

        public string PaymentMethod { get; set; }
        public string PaymentSummary { get; set; }

        public bool IsPaymentSelectionSkipped { get; set; }
    }
}