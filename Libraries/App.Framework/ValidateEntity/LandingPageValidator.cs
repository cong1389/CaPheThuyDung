using System.Text.RegularExpressions;
using App.FakeEntity.Other;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class LandingPageValidator : AbstractValidator<LandingPageViewModel>
	{
		public LandingPageValidator()
		{
			RuleFor(x => x.FullName).NotEmpty().WithMessage("Vui lòng nhập họ tên.");
			RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Vui lòng nhập số điện thoại.");
			RuleFor(x => x.Email).Must(IsValidEmail).WithMessage("Email không đúng định dạng");
			RuleFor(x => x.DateOfBith).NotEmpty().WithMessage("Vui lòng nhập ngày sinh.");
			RuleFor(x => x.ShopId).NotEmpty().WithMessage("Vui lòng chọn cửa hàng.");
		}

		public static bool IsValidEmail(string email)
		{
		    var flag = new Regex("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$").IsMatch(email);
		    return flag;
		}
	}
}