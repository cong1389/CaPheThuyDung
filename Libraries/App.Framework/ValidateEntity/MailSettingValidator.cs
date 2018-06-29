using App.FakeEntity.ServerMail;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class MailSettingValidator : AbstractValidator<ServerMailSettingViewModel>
	{
		public MailSettingValidator()
		{
			RuleFor(x => x.FromAddress).NotEmpty().WithMessage("Vui lòng nhập địa chỉ email.");
			RuleFor(x => x.UserId).NotEmpty().WithMessage("Vui lòng nhập email id.");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Vui lòng nhập mật khẩu email id.");
			RuleFor(x => x.SMTPPort).NotEmpty().WithMessage("Vui lòng nhập port server.");
			RuleFor(x => x.SmtpClient).NotEmpty().WithMessage("Vui lòng nhập địa chỉ stmtp server.");
		}
	}
}