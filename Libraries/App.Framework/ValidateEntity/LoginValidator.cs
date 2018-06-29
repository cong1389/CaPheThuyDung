using App.FakeEntity.User;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class LoginValidator : AbstractValidator<LoginViewModel>
	{
		public LoginValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Vui lòng nhập tên đăng nhập.");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Vui lòng nhập mật khẩu.");
		}
	}
}