using App.FakeEntity.Common;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class AddressValidator : AbstractValidator<AddressViewModel>
	{
		public AddressValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("Vui lòng nhập họ tên.");
			RuleFor(x => x.Address1).NotEmpty().WithMessage("Vui lòng nhập địa chỉ.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Vui lòng nhập số điện thoại.");
        }
	}
}