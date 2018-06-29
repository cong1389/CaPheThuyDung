using App.FakeEntity.Location;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class ProvinceValidator : AbstractValidator<ProvinceViewModel>
	{
		public ProvinceValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
			RuleFor(x => x.OrderDisplay).NotEmpty().WithMessage("Vui lòng nhập vị trí hiển thị.");
		}
	}
}