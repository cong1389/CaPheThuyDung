using App.FakeEntity.Orders;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItemViewModel>
    {
        public ShoppingCartItemValidator()
        {
            RuleFor<int?>(x => x.Quantity).NotEmpty().WithMessage("Vui lòng nhập số lượng.");
        }
    }
}