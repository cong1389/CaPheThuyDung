using App.FakeEntity.Post;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
    public class PostGalleryValidator : AbstractValidator<PostGalleryViewModel>
    {
        public PostGalleryValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Vui lòng nhập Tên.");            
        }
    }
}