using App.Core.Common;
using App.Domain.Common;
using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace App.Domain.Orders
{
    public class Order : AuditableEntity<int>
    {
        public string OrderNumber
        {
            get; set;
        }
        public Guid OrderGuid
        {
            get; set;
        }
        public int StoreId
        {
            get; set;
        }
        public int CustomerId
        {
            get; set;
        }
        [DataMember]
        public virtual Customer Customer { get; set; }

        [DataMember]
        public int BillingAddressId { get; set; }

        /// <summary>
        /// Gets or sets the shipping address identifier
        /// </summary>
		[DataMember]
        public int? ShippingAddressId { get; set; }

        [DataMember]
        public virtual Address BillingAddress { get; set; }

        [DataMember]
        public virtual Address ShippingAddress { get; set; }

        public int OrderStatusId
        {
            get; set;
        }
        public int ShippingStatusId
        {
            get; set;
        }
        public int PaymentStatusId
        {
            get; set;
        }
        public string PaymentMethodSystemName
        {
            get; set;
        }
        public string CustomerCurrencyCode
        {
            get; set;
        }
        public decimal CurrencyRate
        {
            get; set;
        }
        public int CustomerTaxDisplayTypeId
        {
            get; set;
        }
        public string VatNumber
        {
            get; set;
        }
        public decimal OrderSubtotalInclTax
        {
            get; set;
        }
        public decimal OrderSubtotalExclTax
        {
            get; set;
        }
        public decimal OrderSubTotalDiscountInclTax
        {
            get; set;
        }
        public decimal OrderSubTotalDiscountExclTax
        {
            get; set;
        }
        public decimal OrderShippingInclTax
        {
            get; set;
        }
        public decimal OrderShippingExclTax
        {
            get; set;
        }
        public decimal PaymentMethodAdditionalFeeInclTax
        {
            get; set;
        }
        public decimal PaymentMethodAdditionalFeeExclTax
        {
            get; set;
        }
        public string TaxRates
        {
            get; set;
        }
        public decimal OrderTax
        {
            get; set;
        }
        public decimal OrderDiscount
        {
            get; set;
        }
        public decimal OrderTotal
        {
            get; set;
        }
        public decimal RefundedAmount
        {
            get; set;
        }
        public bool RewardPointsWereAdded
        {
            get; set;
        }
        public string CheckoutAttributeDescription
        {
            get; set;
        }
        public string CheckoutAttributesXml
        {
            get; set;
        }
        public int CustomerLanguageId
        {
            get; set;
        }
        public int AffiliateId
        {
            get; set;
        }
        public string CustomerIp
        {
            get; set;
        }
        public bool AllowStoringCreditCardNumber
        {
            get; set;
        }
        public string CardType
        {
            get; set;
        }
        public string CardName
        {
            get; set;
        }
        public string CardNumber
        {
            get; set;
        }
        public string MaskedCreditCardNumber
        {
            get; set;
        }
        public string CardCvv2
        {
            get; set;
        }
        public string CardExpirationMonth
        {
            get; set;
        }
        public string CardExpirationYear
        {
            get; set;
        }
        public bool AllowStoringDirectDebit
        {
            get; set;
        }
        public string DirectDebitAccountHolder
        {
            get; set;
        }
        public string DirectDebitAccountNumber
        {
            get; set;
        }
        public string DirectDebitBankCode
        {
            get; set;
        }
        public string DirectDebitBankName
        {
            get; set;
        }
        public string DirectDebitBIC
        {
            get; set;
        }
        public string DirectDebitCountry
        {
            get; set;
        }
        public string DirectDebitIban
        {
            get; set;
        }
        public string AuthorizationTransactionId
        {
            get; set;
        }
        public string AuthorizationTransactionCode
        {
            get; set;
        }
        public string AuthorizationTransactionResult
        {
            get; set;
        }
        public string CaptureTransactionId
        {
            get; set;
        }
        public string CaptureTransactionResult
        {
            get; set;
        }
        public string SubscriptionTransactionId
        {
            get; set;
        }
        public string PurchaseOrderNumber
        {
            get; set;
        }
        public DateTime PaidDateUtc
        {
            get; set;
        }
        public string ShippingMethod
        {
            get; set;
        }
        public string ShippingRateComputationMethodSystemName
        {
            get; set;
        }
        public bool Deleted
        {
            get; set;
        }
        public DateTime CreatedOnUtc
        {
            get; set;
        }
        public DateTime UpdatedOnUtc
        {
            get; set;
        }
        public int RewardPointsRemaining
        {
            get; set;
        }
        public string CustomerOrderComment
        {
            get; set;
        }
        public decimal OrderShippingTaxRate
        {
            get; set;
        }
        public decimal PaymentMethodAdditionalFeeTaxRate
        {
            get; set;
        }
        public bool HasNewPaymentNotification
        {
            get; set;
        }
        public bool AcceptThirdPartyEmailHandOver
        {
            get; set;
        }

        [DataMember]
        public OrderStatus OrderStatus
        {
            get
            {
                return (OrderStatus)OrderStatusId;
            }
            set
            {
                OrderStatusId = (int)value;
            }
        }

        [DataMember]
        public PaymentStatus PaymentStatus
        {
            get
            {
                return (PaymentStatus)PaymentStatusId;
            }
            set
            {
                PaymentStatusId = (int)value;
            }
        }

        public ShippingStatus ShippingStatus
        {
            get
            {
                return (ShippingStatus)ShippingStatusId;
            }
            set
            {
                ShippingStatusId = (int)value;
            }
        }

        public virtual ICollection<OrderItem> OrderItems
        {
            get; set;
        }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

    }
}
