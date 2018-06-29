using App.Core.Common;
using Domain.Entities.Customers;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Orders
{
    public class ShoppingCartItem : AuditableEntity<int>
    {
        public int StoreId
        {
            get; set;
        }
        public int ParentItemId
        {
            get; set;
        }
        public int BundleItemId
        {
            get; set;
        }

        public int ShoppingCartTypeId
        {
            get; set;
        }

        [ForeignKey("CustomerId")]
        public Customer Customers
        {
            get; set;
        }
        public int CustomerId
        {
            get; set;
        }
        public int PostId
        {
            get; set;
        }
        public string AttributesXml
        {
            get; set;
        }
        public decimal? CustomerEnteredPrice
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
    }
}
