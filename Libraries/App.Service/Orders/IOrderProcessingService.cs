using System.Collections.Generic;

namespace App.Service.Orders
{
   public interface IOrderProcessingService
    {
        PlaceOrderResult PlaceOrder(ProcessPaymentRequest processPaymentRequest, Dictionary<string, string> extraData);
    }
}
