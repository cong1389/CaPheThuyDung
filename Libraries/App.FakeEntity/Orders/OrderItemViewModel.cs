using System;
using App.FakeEntity.Post;

namespace App.FakeEntity.Orders
{
    public class OrderItemViewModel
    {
        public Guid OrderItemGuid
        {
            get; set;
        }
        public int OrderId
        {
            get; set;
        }
        public virtual OrderViewModel Order
        {
            get;
            set;
        }

        public int PostId
        {
            get; set;
        }
        public virtual PostViewModel Post { get; set; }

        public int Quantity
        {
            get; set;
        }
        public decimal UnitPriceInclTax
        {
            get; set;
        }
        public decimal UnitPriceExclTax
        {
            get; set;
        }
        public decimal PriceInclTax
        {
            get; set;
        }
        public decimal PriceExclTax
        {
            get; set;
        }
        public decimal DiscountAmountInclTax
        {
            get; set;
        }
        public decimal DiscountAmountExclTax
        {
            get; set;
        }
        public string AttributeDescription
        {
            get; set;
        }
        public string AttributesXml
        {
            get; set;
        }
        public int DownloadCount
        {
            get; set;
        }
        public bool IsDownloadActivated
        {
            get; set;
        }
        public int LicenseDownloadId
        {
            get; set;
        }
        public decimal ItemWeight
        {
            get; set;
        }
        public string BundleData
        {
            get; set;
        }
        public decimal ProductCost
        {
            get; set;
        }
        public decimal TaxRate
        {
            get; set;
        }
    }
}
