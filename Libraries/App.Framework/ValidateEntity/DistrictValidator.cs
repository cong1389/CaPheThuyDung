using App.FakeEntity.Location;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class DistrictValidator : AbstractValidator<DistrictViewModel>
	{
		public DistrictValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
			RuleFor(x => x.OrderDisplay).NotEmpty().WithMessage("Vui lòng nhập vị trí hiển thị.");
		}
	}
}