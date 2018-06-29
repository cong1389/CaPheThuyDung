using App.FakeEntity.Attribute;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class AttributeValueValidator : AbstractValidator<AttributeValueViewModel>
	{
		public AttributeValueValidator()
		{
			RuleFor(x => x.ValueName).NotEmpty().WithMessage("Vui lòng nhập giá trị thuộc tính.");
			RuleFor(x => x.AttributeId).NotEmpty().WithMessage("Vui lòng chọn thuộc tính.");
		}
	}
}