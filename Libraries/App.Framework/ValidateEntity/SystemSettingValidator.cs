using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using App.FakeEntity.System;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class SystemSettingValidator : AbstractValidator<SystemSettingViewModel>
	{
		public SystemSettingValidator()
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");

            RuleFor(x => x.Logo).Must(IsValidFileType).WithMessage("Hình ảnh Logo không đúng định dạng");

            RuleFor(x => x.LogoFooter).Must(IsValidFileType).WithMessage("Hình ảnh Logo không đúng định dạng");

            RuleFor(x => x.Favicon).Must(IsValidFileType).WithMessage("Hình ảnh Favicon không đúng định dạng");
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