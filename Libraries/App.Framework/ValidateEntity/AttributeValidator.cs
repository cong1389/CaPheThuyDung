using App.FakeEntity.Attribute;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class AttributeValidator : AbstractValidator<AttributeViewModel>
	{
		public AttributeValidator()
		{
			RuleFor(x => x.AttributeName).NotEmpty().WithMessage("Vui lòng nhập tên thuộc tính.");
		}
	}
}