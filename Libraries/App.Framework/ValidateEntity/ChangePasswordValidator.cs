using App.FakeEntity.User;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
	{
		public ChangePasswordValidator()
		{
			RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Vui lòng nhập mật khẩu cũ.");
			RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Vui lòng nhập mật khẩu mới.");
			RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Vui lòng nhập xác nhận mật khẩu.");
		}
	}
}