using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using App.Aplication;
using App.Domain.Common;
using App.Domain.GlobalSetting;
using App.Front.Extensions;
using App.Front.Models;
using App.Service.ContactInformation;
using App.Service.MailSetting;
using App.Service.Menu;
using App.Service.Messages;
using App.Service.SystemApp;

namespace App.Front.Controllers
{
    public class ContactController : FrontBaseController
    {
        private readonly IContactInfoService _contactInfoService;
        private readonly ISystemSettingService _systemSettingService;
        private readonly ISendMailService _sendMailService;

        public ContactController(IContactInfoService contactInfoService, IMenuLinkService menuLinkService
            , IMailSettingService mailSettingService
            , ISystemSettingService systemSettingService
            , ISendMailService sendMailService)
        {
            _contactInfoService = contactInfoService;
            _systemSettingService = systemSettingService;
            _sendMailService = sendMailService;
        }

        [PartialCache("Long")]
        public ActionResult ContactUs()
        {
            var contactInformation = _contactInfoService.FindBy(x => x.Status == (int)Status.Enable, true);

            if (contactInformation == null)
            {
                return HttpNotFound();
            }

            var contactInformationLocalize = contactInformation.Select(x => x.ToModel());
            
            //ViewBag.Title = title;
            ViewBag.Contact = contactInformationLocalize;

            var contact = new ContactForm();

            return PartialView(contact);
        }

        [PartialCache("Long")]
        public ActionResult HomeContact()
        {
            var contactInformation = _contactInfoService.FindBy(x => x.Status == (int)Status.Enable, true);

            if (contactInformation == null)
            {
                return HttpNotFound();
            }

            var contactInformationLocalize = contactInformation.Select(x => x.ToModel());
            
            return PartialView(contactInformationLocalize);
        }

        public ActionResult SendContact(ContactForm model)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                {
                    return Json(new
                    {
                        title="Gửi liên hệ",
                        success = false,
                        errors = string.Join(", ", ModelState.Values.SelectMany(v =>
                            from x in v.Errors
                            select x.ErrorMessage).ToArray())
                    });
                }

                string serverPathEmail = string.Concat(Server.MapPath("\\"), Contains.TemplateMailBasicContact);
                if (System.IO.File.Exists(serverPathEmail))
                {
                    const string messageId = "Email";
                    string subject = "Thông tin liên hệ";

                    var body = string.Concat("", "Người gửi: ", model.FullName, "<br>");
                    body = string.Concat(body, string.Concat("E-mail: ", model.Email), "<br>");
                    body = string.Concat(body, model.Content, "<br>");

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(serverPathEmail);
                    XmlNode xmlNodes = xmlDocument.SelectSingleNode(string.Concat("MailFormats/MailFormat[@Id='", messageId, "']"));

                    string subjectNodes = xmlNodes.SelectSingleNode("Subject")?.InnerText;
                    subjectNodes = subject.Replace(string.Concat(subject, "?"), subject);

                    string bodyNodes = xmlNodes.SelectSingleNode("Body")?.InnerText;
                    bodyNodes = body.Replace(string.Concat(body, "?"), body);

                    var toEmail = _systemSettingService.Get(x => x.Status == 1).Email;

                    var sendMail = new SendMail
                    {
                        MessageId = "Email",
                        ToEmail = toEmail,
                        Subject = subjectNodes,
                        Body = bodyNodes
                    };

                     _sendMailService.SendMailSmtp(sendMail);

                    model.SuccessfullySent = true;
                    model.Result = "Gửi liên hệ thành công, chúng tôi sẽ liên lạc với bạn ngay khi có thể.";
                }

                return Json(new
                {
                    title = "Gửi liên hệ",
                    success = true,
                    message = "Gửi liên hệ thành công, chúng tôi sẽ liên lạc với bạn ngay khi có thể."
                });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}