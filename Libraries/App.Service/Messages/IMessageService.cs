
namespace App.Service.Messages
{
	public class CreateMessageResult
	{
		public MessageContext MessageContext { get; set; }
	}

	public interface IMessageService
	{
		CreateMessageResult CreateMessage(MessageContext messageContext, bool queue, params object[] modelParts);
	}
}
