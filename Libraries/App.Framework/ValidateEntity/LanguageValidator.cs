using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using App.FakeEntity.Language;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class LanguageValidator : AbstractValidator<LanguageFormViewModel>
	{
		public LanguageValidator()
		{
			RuleFor(x => x.LanguageName).NotEmpty().WithMessage("Vui lòng nhập tên ngôn ngữ.");
			RuleFor(x => x.LanguageCode).NotEmpty().WithMessage("Vui lòng nhập mã ngôn ngữ.");
			RuleFor(x => x.File).Must(IsValidFileType).WithMessage("Hình ảnh không đúng định dạng");
		}

        public static bool IsValidFileType(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0) return true;

            ImageFormat[] jpeg =
                {ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif, ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Tiff};
            using (Image image = Image.FromStream(file.InputStream))
            {
                if (!jpeg.Contains(image.RawFormat))
                {
                    return false;
                }
            }

            return true;
        }
    }
}