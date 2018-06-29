using App.FakeEntity.GenericControl;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class GenericControlValidator : AbstractValidator<GenericControlViewModel>
	{
		public GenericControlValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Vui lòng nhập tên thuộc tính.");
		}
	}
}