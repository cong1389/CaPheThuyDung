

namespace App.Service.Messages
{
    public interface IMessageModelProvider
    {
        void AddGlobalModelParts(MessageContext messageContext);
        void AddModelPart(object part, MessageContext messageContext, string name = null);
        string ResolveModelName(object model);
    }
}