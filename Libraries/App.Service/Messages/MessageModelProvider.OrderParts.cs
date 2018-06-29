using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using App.Core.ComponentModel;
using App.Core.Extensions;
using App.Domain.Orders;
using App.Service.Addresses;

namespace App.Service.Messages
{
    public partial class MessageModelProvider
	{
		protected virtual object CreateModelPart(Order part, MessageContext messageContext)
		{
			var allow = new HashSet<string>
			{
				nameof(part.Id),
				nameof(part.OrderNumber),
				nameof(part.OrderGuid),
				nameof(part.StoreId),
				nameof(part.OrderStatus),
				nameof(part.PaymentStatus),
				nameof(part.ShippingStatus),
                nameof(part.VatNumber),
				nameof(part.AffiliateId),
				nameof(part.CustomerIp),
				nameof(part.CardType),
				nameof(part.CardName),
				nameof(part.MaskedCreditCardNumber),
				nameof(part.DirectDebitAccountHolder),
				nameof(part.DirectDebitBankCode), // TODO: (mc) Liquid > Bank data (?)
				nameof(part.PurchaseOrderNumber),
				nameof(part.ShippingMethod),
				nameof(part.PaymentMethodSystemName),
				nameof(part.ShippingRateComputationMethodSystemName)
				// TODO: (mc) Liquid > More whitelisting?
			};

			var m = new HybridExpando(part, allow, MemberOptMethod.Allow);
			var d = (dynamic)m;

			d.ID = part.Id;
			d.Billing = CreateModelPart(part.BillingAddress, messageContext);
			if (part.ShippingAddress != null)
			{
				d.Shipping = part.ShippingAddress.IsPostalDataEqual(part.BillingAddress) ? null : CreateModelPart(part.ShippingAddress, messageContext);
			}
			d.CustomerEmail = part.BillingAddress.Email.NullEmpty();
			d.CustomerComment = part.CustomerOrderComment.NullEmpty();
			d.Status = part.OrderStatus.ToString();
			d.CreatedOn = part.CreatedOnUtc;

			// Payment method
			var paymentMethodName = part.PaymentMethodSystemName;
			d.PaymentMethod = paymentMethodName.NullEmpty();
			
			m.Properties["OrderNumber"] = part.Id;
			
			d.Items = part.OrderItems.Select(x => CreateModelPart(x, messageContext)).ToList();
			d.Totals = CreateOrderTotalsPart(part, messageContext);

			return m;
		}

		protected virtual object CreateModelPart(OrderItem part, MessageContext messageContext)
		{
			var m = new Dictionary<string, object>
			{
				{ "Qty", part.Quantity },
				{ "UnitPrice", part.UnitPriceInclTax },
				{ "LineTotal", part.PriceInclTax},
				{ "PostName",part.Post.Title  }
			};


			return m;
		}

		protected virtual object CreateOrderTotalsPart(Order order, MessageContext messageContext)
		{
			dynamic m = new ExpandoObject();

			m.SubTotal = order.OrderSubtotalExclTax;

			m.Total = order.OrderTotal;

			return m;
		}

    }
}
