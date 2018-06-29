using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.ServerMail
{
	public class ServerMailSettingViewModel
	{
		[Display(Name="EnableSSL", ResourceType=typeof(FormUI))]
		public bool EnableSSL
		{
			get;
			set;
		}

		[Display(Name="FromAddress", ResourceType=typeof(FormUI))]
		public string FromAddress
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		[Display(Name="Password", ResourceType=typeof(FormUI))]
		public string Password
		{
			get;
			set;
		}

		[Display(Name="SmtpClient", ResourceType=typeof(FormUI))]
		public string SmtpClient
		{
			get;
			set;
		}

		[Display(Name="SMTPPort", ResourceType=typeof(FormUI))]
		public string SMTPPort
		{
			get;
			set;
		}

		[Display(Name="Status", ResourceType=typeof(FormUI))]
		public int Status
		{
			get;
			set;
		}

		[Display(Name="UserId", ResourceType=typeof(FormUI))]
		public string UserId
		{
			get;
			set;
		}
	}
}