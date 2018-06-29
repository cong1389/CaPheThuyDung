using App.FakeEntity.Brandes;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class BrandValidator : AbstractValidator<BrandViewModel>
	{
		public BrandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
			RuleFor(x => x.OrderDisplay).NotEmpty().WithMessage("Vui lòng nhập vị trí hiển thị.");
		}
	}
}