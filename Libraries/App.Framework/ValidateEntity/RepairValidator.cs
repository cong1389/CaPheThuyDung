using App.FakeEntity.Repairs;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class RepairValidator : AbstractValidator<RepairViewModel>
    {
        public RepairValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Vui lòng nhập tên bạn.");
            RuleFor(x => x.BrandId).NotEmpty().WithMessage("Vui lòng chọn thương hiệu.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Vui lòng nhập số điện thoại.");
            RuleFor(x => x.ModelBrand).NotEmpty().WithMessage("Vui lòng nhập dòng máy.");
        }
    }
}