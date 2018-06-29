using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Xml;

namespace App.Front.Models
{
	public class SendMail
	{
		private string _fromAddress;

		private string _password;

		private int _smtpPort;

		private string _userId;

		private bool _enableSsl;

		private string _strSmtpClient;

	    public void InitMail(string fromAddress, string smtpClient, string userId, string password, string sMtpPort, bool enableSsl)
		{
			try
			{
				_fromAddress = fromAddress;
				_strSmtpClient = smtpClient;
				_userId = userId;
				_password = password;
				_smtpPort = int.Parse(sMtpPort);
				_enableSsl = enableSsl;
			}
			catch 
			{
			}
		}

		public void SendToMail(string messageId, string toAddress, string[] param)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string str = string.Concat(HttpContext.Current.Server.MapPath("\\"), "Mailformat.xml");
		    int i;
			if (File.Exists(str))
			{
				xmlDocument.Load(str);
				XmlNode xmlNodes = xmlDocument.SelectSingleNode(string.Concat("MailFormats/MailFormat[@Id='", messageId, "']"));
				var innerText = xmlNodes.SelectSingleNode("Subject").InnerText;
				var innerText1 = xmlNodes.SelectSingleNode("Body").InnerText;
				if (param == null)
				{
					throw new Exception("Mail format file not found.");
				}
				for (i = 0; i <= param.Length - 1; i++)
				{
					innerText1 = innerText1.Replace(string.Concat(i.ToString(), "?"), param[i]);
					innerText = innerText.Replace(string.Concat(i.ToString(), "?"), param[i]);
				}
				dynamic mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(_fromAddress);
				mailMessage.To.Add(toAddress);
				mailMessage.Subject = innerText;
				mailMessage.IsBodyHtml = true;
				mailMessage.Body = innerText1;
				SmtpClient smtpClient = new SmtpClient
				{
					Host = _strSmtpClient,
					EnableSsl = _enableSsl,
					Port = Convert.ToInt32(_smtpPort),
					Credentials = new NetworkCredential(_userId, _password)
				};
				try
				{
					smtpClient.Send(mailMessage);
				}
				catch (SmtpFailedRecipientsException smtpFailedRecipientsException1)
				{
					SmtpFailedRecipientsException smtpFailedRecipientsException = smtpFailedRecipientsException1;
					for (int j = 0; j <= smtpFailedRecipientsException.InnerExceptions.Length; j++)
					{
						SmtpStatusCode statusCode = smtpFailedRecipientsException.InnerExceptions[j].StatusCode;
						if (statusCode == SmtpStatusCode.MailboxBusy | statusCode == SmtpStatusCode.MailboxUnavailable)
						{
							Thread.Sleep(2000);
							smtpClient.Send(mailMessage);
						}
					}
				}
			}
		}
	}
}