using App.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.GlobalSetting
{
	public class ServerMailSetting : AuditableEntity<int>
	{
		public bool EnableSSL
		{
			get;
			set;
		}

		[StringLength(250)]
		public string FromAddress
		{
			get;
			set;
		}

		[StringLength(250)]
		public string Password
		{
			get;
			set;
		}

		[StringLength(250)]
		public string SmtpClient
		{
			get;
			set;
		}

		[StringLength(50)]
		public string SMTPPort
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		[StringLength(250)]
		public string UserID
		{
			get;
			set;
		}

		public ServerMailSetting()
		{
		}
	}
}