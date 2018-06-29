using System.IO;
using System.Linq;
using System.Web;
using App.FakeEntity.Slide;
using FluentValidation;

namespace App.Framework.ValidateEntity
{
	public class SlideShowValidator : AbstractValidator<SlideShowViewModel>
	{
		public SlideShowValidator()
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Vui lòng nhập tiêu đề.");
			RuleFor(x => x.OrderDisplay).NotEmpty().WithMessage("Vui lòng nhập xắp xếp vị trí.");
			RuleFor(x => x.Image).Must(IsValidFileType).WithMessage("Tệp tin không đúng định dạng.");
		}

		public static bool IsValidFileType(HttpPostedFileBase file)
		{
			bool flag;
			if (file == null || file.ContentLength <= 0)
			{
				flag = true;
			}
			else
			{
				flag = new[] { ".jpg", ".png", ".gif", ".jpeg", ".mp4" }.Contains(Path.GetExtension(file.FileName));
			}
			return flag;
		}
	}
}