using App.FakeEntity.User;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class RegisterValidation : AbstractValidator<RegisterFormViewModel>
	{
		public RegisterValidation()
		{
            //RuleFor((RegisterFormViewModel x) => x.FirstName).NotEmpty().WithMessage("Vui lòng nhập tên.").Length(1, 50).WithMessage("Tên phải từ 1 đến 50 ký tự.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Vui lòng nhập tên tài khoản").Length(4, 50).WithMessage("Tên tài khoản phải từ 4 đến 50 ký tự.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Vui lòng nhập mật khẩu tài khoản.");
            RuleFor(x => x.Password).Length(6, 32).WithMessage("Mật khẩu phải có ít nhất từ 6 ký tự trở lên.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Vui lòng xác nhận mật khẩu.").Equal(x => x.Password, null).WithMessage("Mật khẩu xác nhận không chính xác");
            //RuleFor((RegisterFormViewModel x) => x.Email).NotEmpty().WithMessage("Vui lòng nhập email tài khoản.");
            //RuleFor((RegisterFormViewModel x) => x.Email).EmailAddress().WithMessage("EMail không đúng định dạng");
            //RuleFor((RegisterFormViewModel x) => x.Phone).Length(10, 12).WithMessage("Số điện thoại phải từ 10-12 ký tự");
		}
	}
}