using App.FakeEntity.Ads;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	internal class PageBannerValidator : AbstractValidator<PageBannerViewModel>
	{
		public PageBannerValidator()
		{
			RuleFor(x => x.PageName).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
			RuleFor(x => x.OrderDisplay).NotEmpty().WithMessage("Vui lòng nhập vị trí hiển thị.");
		}
	}
}