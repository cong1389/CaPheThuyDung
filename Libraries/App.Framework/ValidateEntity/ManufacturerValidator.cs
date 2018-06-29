using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using App.FakeEntity.Manufacturers;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class ManufacturerValidator : AbstractValidator<ManufacturerViewModel>
	{
		public ManufacturerValidator()
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
			RuleFor(x => x.Image).Must(IsValidFileType).WithMessage("Hình ảnh không đúng định dạng");
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