using System.Text.RegularExpressions;
using App.FakeEntity.ContactInformation;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class ContactInformationValidator : AbstractValidator<ContactInformationViewModel>
	{
		public ContactInformationValidator()
		{
            RuleFor(x => x.Title).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Vui lòng nhập địa chỉ.");
            RuleFor(x => x.Email).Must(IsValidEmail).WithMessage("Email không đúng định dạng");
            RuleFor(x => x.OrderDisplay).NotEmpty().WithMessage("Vui lòng nhập vị trí hiển thị.");
            RuleFor(x => x.OrderDisplay).GreaterThanOrEqualTo(0).WithMessage("Vị trí hiển thị phải là số và lớn hơn 0.");
		}

		public static bool IsValidEmail(string email)
		{
		    var flag = new Regex(
		            "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$")
		        .IsMatch(email);
		    return flag;
		}
	}
}