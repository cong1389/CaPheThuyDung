using App.FakeEntity.GenericControl;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class GenericControlValueValidator : AbstractValidator<GenericControlValueViewModel>
	{
		public GenericControlValueValidator()
		{
			RuleFor(x => x.ValueName).NotEmpty().WithMessage("Vui lòng nhập giá trị thuộc tính.");
			RuleFor(x => x.GenericControlId).NotEmpty().WithMessage("Vui lòng chọn thuộc tính.");
		}
	}
}