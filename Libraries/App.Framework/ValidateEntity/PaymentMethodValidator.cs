using App.FakeEntity.Payments;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class PaymentMethodValidator : AbstractValidator<PaymentMethodViewModel>
	{
		public PaymentMethodValidator()
		{
			RuleFor(x => x.PaymentMethodSystemName).NotEmpty().WithMessage("Vui lòng chọn hình thức thanh toán.");
            
		}
	}
}