using App.Core.Messages;
using App.Domain.Orders;

namespace App.Service.Messages
{
	public static class OrderMessageFactoryExtensions
    {
        public static CreateMessageResult SendOrderPlacedStoreOwnerNotification(this IMessageService factory, Order order, int languageId = 0)
        {
            return factory.CreateMessage(MessageContext.Create(MessageTemplateNames.OrderPlacedStoreOwner, languageId, order.StoreId), true, order, order.Customer);
        }

        public static CreateMessageResult SendOrderPlacedCustomerNotification(this IMessageService factory, Order order, int languageId = 0)
        {
            return factory.CreateMessage(MessageContext.Create(MessageTemplateNames.OrderPlacedCustomer, languageId, order.StoreId, order.Customer), true, order);
        }
    }
}
