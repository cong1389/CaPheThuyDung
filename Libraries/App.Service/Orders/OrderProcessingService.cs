using System;
using System.Collections.Generic;
using System.Linq;
using App.Aplication;
using App.Domain.Common;
using App.Domain.Orders;
using App.Service.Customers;
using App.Service.GenericAttribute;
using App.Service.Messages;
using App.Service.Post;

namespace App.Service.Orders
{
	public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IMessageService _messageFactory;
        private readonly IPostService _postService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;

        public OrderProcessingService(IOrderService orderService
            , IShoppingCartItemService shoppingCartItemService
            , ICustomerService customerService
            , IGenericAttributeService genericAttributeService
            , IPriceCalculationService priceCalculationService
            , IMessageService messageFactory, IPostService postService
            , IOrderTotalCalculationService orderTotalCalculationService)
        {
            _orderService = orderService;
            _shoppingCartItemService = shoppingCartItemService;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _priceCalculationService = priceCalculationService;
            _messageFactory = messageFactory;
            _postService = postService;
            _orderTotalCalculationService = orderTotalCalculationService;
        }

        public virtual PlaceOrderResult PlaceOrder(
           ProcessPaymentRequest processPaymentRequest,
           Dictionary<string, string> extraData)
        {
            var result = new PlaceOrderResult();
            var utcNow = DateTime.UtcNow;

            if (processPaymentRequest == null)
            {
                throw new AggregateException("processPaymentRequest");
            }

            if (processPaymentRequest.OrderGuid == Guid.Empty)
            {
                processPaymentRequest.OrderGuid = Guid.NewGuid();
            }

            try
            {
                var customer = _customerService.GetById(processPaymentRequest.CustomerId, false);
                var cart = customer.GetCartItems();

                //Order total 
                decimal? orderTotal = null;
                if (cart != null)
                {
                    var subTotal = _shoppingCartItemService.GetCurrentCartSubTotal(cart);
                    orderTotal = subTotal;
                }

                //BillingAddress  
                var billingAddress = customer.BillingAddress;

                //Shiping method
                var shippingMethod = customer.GetAttribute("Customer", Contains.SelectedShippingOption, _genericAttributeService);

                var order = new Order
                {
                    StoreId = processPaymentRequest.StoreId,
                    OrderGuid = processPaymentRequest.OrderGuid,
                    CustomerId = customer.Id,
                    CustomerLanguageId = 1, // Draft code
                    CustomerTaxDisplayTypeId = 0, //Draft code
                    CustomerIp = "172.0.0.0", //Draft code
                    OrderSubtotalInclTax = 0, //Draft code
                    OrderSubtotalExclTax = _orderTotalCalculationService.GetShoppingCartSubTotal(cart), //Draft code
                    OrderSubTotalDiscountInclTax = decimal.Zero, //Draft code
                    OrderSubTotalDiscountExclTax = decimal.Zero,//Draft code
                    OrderShippingInclTax = decimal.Zero,//Draft code
                    OrderShippingExclTax = decimal.Zero,//Draft code
                    OrderShippingTaxRate = decimal.Zero,//Draft code
                    PaymentMethodAdditionalFeeInclTax = decimal.Zero,//Draft code
                    PaymentMethodAdditionalFeeExclTax = decimal.Zero,//Draft code
                    PaymentMethodAdditionalFeeTaxRate = decimal.Zero,//Draft code
                    TaxRates = string.Empty,//Draft code
                    OrderTax = decimal.Zero,//Draft code
                    BillingAddress = billingAddress,
                    PaidDateUtc = utcNow,
                    CreatedOnUtc = utcNow,
                    UpdatedOnUtc = utcNow,
                    PaymentMethodSystemName = processPaymentRequest.PaymentMethodSystemName,
                    CustomerCurrencyCode = "VNĐ",
                    CurrencyRate = decimal.Zero,
                    OrderTotal = orderTotal.Value,
                    ShippingMethod = shippingMethod,
                    ShippingStatus = ShippingStatus.ShippingNotRequired,
                    OrderStatus = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Pending

                };

                _orderService.Create(order);

                result.PlacedOrder = order;

                //Insert OrderItem
                foreach (var sc in cart)
                {
                    var scSubTotal = _priceCalculationService.GetSubTotal(sc, true);
                    var scSubTotalInclTax = scSubTotal;

                    var orderItem = new OrderItem
                    {
                        OrderItemGuid = Guid.NewGuid(),
                        Order = order,
                        PostId = sc.PostId,
                        Quantity = sc.Quantity,
                        UnitPriceInclTax = sc.CustomerEnteredPrice,
                        UnitPriceExclTax = decimal.Zero,
                        PriceExclTax = decimal.Zero,
                        PriceInclTax = scSubTotalInclTax,
                        //Post = _postService.GetById(sc.PostId),
                        ItemWeight = decimal.Zero,
                        DiscountAmountInclTax= decimal.Zero,
                        DiscountAmountExclTax= decimal.Zero
                    };

                    order.OrderItems.Add(orderItem);
                    _orderService.Update(order);
                }

                if (result.Success)
                {
                    //Delete
                    cart.ToList().ForEach(cr => _shoppingCartItemService.DeleteShoppingCartItem(cr, false));

                    _customerService.ResetCheckoutData(customer, 1, false, true);

                    #region Notifications, notes and attributes

                    //send email notifications
                    var msg = _messageFactory.SendOrderPlacedStoreOwnerNotification(order, 1);
	                msg = _messageFactory.SendOrderPlacedCustomerNotification(order, order.CustomerLanguageId);

					#endregion


				}
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                // ignored
            }

            return result;
        }
    }
}
