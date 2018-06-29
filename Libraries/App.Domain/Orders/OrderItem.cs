using App.Core.Common;
using App.Domain.Entities.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Orders
{
    public class OrderItem : AuditableEntity<int>
    {
        public Guid OrderItemGuid
        {
            get; set;
        }
        public int OrderId
        {
            get; set;
        }

        [ForeignKey("OrderId")]
        public virtual Order Order
        {
            get;
            set;
        }

        public int PostId
        {
            get; set;
        }
        public virtual Post Post { get; set; }

        public int Quantity
        {
            get; set;
        }
        public decimal? UnitPriceInclTax
        {
            get; set;
        }
        public decimal? UnitPriceExclTax
        {
            get; set;
        }
        public decimal? PriceInclTax
        {
            get; set;
        }
        public decimal? PriceExclTax
        {
            get; set;
        }
        public decimal? DiscountAmountInclTax
        {
            get; set;
        }
        public decimal? DiscountAmountExclTax
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
        public decimal PostCost
        {
            get; set;
        }
        public decimal TaxRate
        {
            get; set;
        }

        public OrderItem()
        {
        }
    }
}
