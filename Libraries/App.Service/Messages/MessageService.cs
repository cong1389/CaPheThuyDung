using App.Aplication;
using App.Core.Extensions;
using App.Core.Templating;
using App.Core.Utilities;
using App.Domain.Email;
using App.Domain.Entities.GlobalSetting;
using App.Domain.GlobalSetting;
using App.Domain.Orders;
using App.Service.Common;
using App.Service.Language;
using App.Service.MailSetting;
using App.Service.Post;
using App.Service.SystemApp;
using Domain.Entities.Customers;
using System;
using System.IO;
using System.Linq;
using App.Core.Messages;

namespace App.Service.Messages
{
	public class MessageService : IMessageService
    {
        private readonly ICommonServices _services;
        private readonly ILanguageService _languageService;
        private readonly ITemplateEngine _templateEngine;
        private readonly IMailSettingService _mailSettingService;
        private readonly IMessageModelProvider _modelProvider;
        private readonly ITemplateManager _templateManager;
        private readonly ISendMailService _sendMailService;
        private readonly IPostService _postService;

        public MessageService(ICommonServices services, ILanguageService languageService,
            ITemplateEngine templateEngine, IMailSettingService mailSettingService, IMessageModelProvider modelProvider
            , ITemplateManager templateManager, ISendMailService sendMailService, IPostService postService)
        {
            _services = services;
            _templateEngine = templateEngine;
            _mailSettingService = mailSettingService;
            _languageService = languageService;
            _modelProvider = modelProvider;
            _templateManager = templateManager;
            _sendMailService = sendMailService;
            _postService = postService;
        }

        public virtual CreateMessageResult CreateMessage(MessageContext messageContext, bool queue,
            params object[] modelParts)
        {
            ValidateMessageContext(messageContext, ref modelParts);

            // Create and assign model
            var model = messageContext.Model = new TemplateModel();

            // Add all global template model parts
            _modelProvider.AddGlobalModelParts(messageContext);

            // Add specific template models for passed parts
            foreach (var part in modelParts)
            {
                _modelProvider.AddModelPart(part, messageContext);
            }

            var messageTemplate = messageContext.MessageTemplate;

            // Render templates
            //var to = RenderEmailAddress(messageTemplate.To, messageContext);
            //var replyTo = RenderEmailAddress(messageTemplate.ReplyTo, messageContext, false);
            //var bcc = RenderTemplate(messageTemplate.BccEmailAddresses, messageContext, false);

            var subject = RenderTemplate(messageTemplate.Subject, messageContext);
            ((dynamic)model).Email.Subject = subject;

            var body = RenderBodyTemplate(messageContext);

            // CSS inliner
            body = InlineCss(body);

            var mail = new SendMail
            {
                Subject = subject,
                Body = body,
                MessageId = "mail",
                ToEmail = messageContext.EmailAccount.FromAddress
            };

            _sendMailService.SendMailSmtp(mail);

            return new CreateMessageResult { MessageContext = messageContext };
        }

        private EmailAddress RenderEmailAddress(string email, MessageContext ctx, bool required = true)
        {
            try
            {
                var parsed = RenderTemplate(email, ctx, required);

                if (required || parsed != null)
                {
                    return parsed.Convert<EmailAddress>();
                }

                return null;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

            }

            return null;
        }

        private string RenderTemplate(string template, MessageContext ctx, bool required = true)
        {
            if (!required && template.IsEmpty())
            {
                return null;
            }

            return _templateEngine.Render(template, ctx.Model, ctx.FormatProvider);
        }

        private void ValidateMessageContext(MessageContext ctx, ref object[] modelParts)
        {
            var t = ctx.MessageTemplate;
            if (t != null)
            {
                if (t.To.IsEmpty() || t.Subject.IsEmpty() || t.Name.IsEmpty())
                {
                    throw new InvalidOperationException("Message template validation failed, because at least one of the following properties has not been set: Name, To, Subject.");
                }
            }

            if (ctx.BaseUri == null)
            {
                ctx.BaseUri = new Uri(Aplication.Utils.GetBaseUrl);
            }

            if (ctx.LanguageId.GetValueOrDefault() == 0)
            {
                ctx.Language = _services.WorkContext.WorkingLanguage;
                ctx.LanguageId = ctx.Language.Id;
            }
            else
            {
                ctx.Language = _languageService.GetById(ctx.LanguageId.Value);
            }

            var parts = modelParts?.AsEnumerable() ?? Enumerable.Empty<object>();

            if (ctx.MessageTemplate == null)
            {
                if (ctx.MessageTemplateName.IsEmpty())
                {
                    throw new ArgumentException("'MessageTemplateName' must not be empty if 'MessageTemplate' is null.", nameof(ctx));
                }

                ctx.MessageTemplate = MessageTemplateConverter.Load(ctx.MessageTemplateName);
                if (ctx.MessageTemplate == null)
                {
                    throw new FileNotFoundException("The message template '{0}' does not exist.".FormatInvariant(ctx.MessageTemplateName));
                }
            }

            if (ctx.EmailAccount == null)
            {
                ctx.EmailAccount = GetEmailAccountOfMessageTemplate(ctx.MessageTemplate);
            }

            if (ctx.SystemSettings == null)
            {
                ctx.SystemSettings = _services.Resolve<ISystemSettingService>().Get(x => x.Status == 1);
            }

            if (ctx.Customer == null)
            {
                // Try to move Customer from parts to MessageContext
                var customer = parts.OfType<Customer>().FirstOrDefault();
                if (customer != null)
                {
                    // Exclude the found customer from parts list
                    parts = parts.Where(x => !object.ReferenceEquals(x, customer));
                }

                ctx.Customer = customer ?? _services.WorkContext.CurrentCustomer;
            }

            var orderDefault = parts.OfType<Order>().FirstOrDefault();
            if (orderDefault != null)
            {
                foreach (var item in orderDefault.OrderItems)
                {
                    item.Post = _postService.GetById(item.PostId);
                }
            }

            modelParts = parts.ToArray();
        }

        protected ServerMailSetting GetEmailAccountOfMessageTemplate(MessageTemplate messageTemplate)
        {
            var account = _mailSettingService.GetActive();

            if (account == null)
            {
                throw new Exception("Email account is null");
            }

            return account;
        }

        private string RenderBodyTemplate(MessageContext ctx)
        {
            var key = BuildTemplateKey();
            var source = ctx.MessageTemplate.Body;
            var template = _templateManager.GetOrAdd(key, GetBodyTemplate);

            return template.Render(ctx.Model, ctx.FormatProvider);

            string GetBodyTemplate()
            {
                return source;
            }
        }

        private string BuildTemplateKey()
        {
            return "MessageTemplate/Body";
        }

        private string InlineCss(string html)
        {
            Uri baseUri = null;

            try
            {
                // 'Store' is a global model part, so we pretty can be sure it exists
                baseUri = new Uri(Utils.GetBaseUrl);
            }
            catch { }

            var pm = new PreMailer.Net.PreMailer(html, baseUri);
            var result = pm.MoveCssInline(false, "#ignore");
            return result.Html;
        }

    }
}
