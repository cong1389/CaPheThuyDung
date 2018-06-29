namespace App.Domain.GlobalSetting
{
	public class SendMail
    {
        public string MessageId { get; set; }
        public string ToEmail{ get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
