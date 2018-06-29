using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using App.Domain.GlobalSetting;
using App.Service.MailSetting;
using Microsoft.AspNet.Identity;

namespace App.Service.Messages
{
	public class SendMailService : IIdentityMessageService, ISendMailService
    {
        private readonly IMailSettingService _mailSettingService;

        public SendMailService(IMailSettingService mailSettingService)
        {
            _mailSettingService = mailSettingService;
        }

        public Task SendAsync(IdentityMessage message)
        {
            return SendMailTrap(message);
        }

        public Task SendMailSmtp(SendMail message)
        {
            if (message== null)
            {
                return Task.FromResult(0);
            }

            var mailSetting = _mailSettingService.Get(x => x.Status == 1);

            var mailMessage = new MailMessage {From = new MailAddress(mailSetting.FromAddress)};
            mailMessage.To.Add(message.ToEmail);
            mailMessage.Subject = message.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message.Body;

            var smtpClient = new SmtpClient
            {
                Host = mailSetting.SmtpClient,
                EnableSsl = mailSetting.EnableSSL,
                Port = Convert.ToInt32(mailSetting.SMTPPort),
                Credentials = new NetworkCredential(mailSetting.UserID, mailSetting.Password)
            };
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientsException smtpFailedRecipientsException1)
            {
                var smtpFailedRecipientsException = smtpFailedRecipientsException1;
                for (int j = 0; j <= smtpFailedRecipientsException.InnerExceptions.Length; j++)
                {
                    var statusCode = smtpFailedRecipientsException.InnerExceptions[j].StatusCode;
                    if (statusCode == SmtpStatusCode.MailboxBusy | statusCode == SmtpStatusCode.MailboxUnavailable)
                    {
                        Thread.Sleep(2000);
                        smtpClient.Send(mailMessage);
                    }
                }
            }

            return Task.FromResult(1);
        }

        public Task SendMailTrap(IdentityMessage message)
        {
            if (message == null)
            {
                return Task.FromResult(0);
            }

            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3ae049f5293edc", "c9036146acfb85"),
                EnableSsl = true
            };

            var from = new MailAddress("ddemo9698@gmail.com", "My Awesome Admin");
            var to = new MailAddress(message.Destination);

            var mail = new MailMessage(from, to)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            client.Send(mail);

            return Task.FromResult(0);
        }
    }
}
