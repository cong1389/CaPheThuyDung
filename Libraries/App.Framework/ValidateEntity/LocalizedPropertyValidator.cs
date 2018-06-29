using App.FakeEntity.Language;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class LocalizedPropertyValidator : AbstractValidator<LocalizedPropertyViewModel>
	{
		public LocalizedPropertyValidator()
		{
			RuleFor(x => x.LocaleValue).NotEmpty().WithMessage("Vui lòng nhập gía trị Localized.");						
		}
		
	}
}