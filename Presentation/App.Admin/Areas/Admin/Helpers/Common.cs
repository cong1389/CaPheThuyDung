using App.Domain.Common;
using Resources;
using System;

namespace App.Admin.Helpers
{
	public static class Common
	{
		public static string GetOrderStatus(int type)
		{
			switch (type)
			{
				case 1:
				{
					return "Đang sửa";
				}
				case 2:
				{
					return "Đang kiểm tra";
				}
				case 3:
				{
					return "Hoàn thành";
				}
			}
			return string.Empty;
		}

		public static string GetPosition(int position)
		{
			switch (position)
			{
				case 1:
				{
					return Position.Top.ToString();
				}
				case 2:
				{
					return Position.Footer.ToString();
				}
				case 3:
				{
					return Position.Left.ToString();
				}
				case 4:
				{
					return Position.Right.ToString();
				}
				case 5:
				{
					return Position.SiderBar.ToString();
				}
				case 6:
				{
					return Position.Middle.ToString();
				}
				case 7:
				{
					return Position.TopHead.ToString();
				}
				default:
				{
					return string.Empty;
				}
			}
		}

		public static string GetStatusLanddingPage(int type)
		{
			switch (type)
			{
				case 1:
				{
					return "Mới đăng ký";
				}
				case 2:
				{
					return "Huỷ nhận quà";
				}
				case 3:
				{
					return "Đã nhận quà";
				}
			}
			return string.Empty;
		}

		public static string GetTypeMenu(int type)
		{
			if (type == 0)
			{
				return FormUI.DisplayListItem;
			}
			if (type == 1)
			{
				return FormUI.ShowContent;
			}
			return string.Empty;
		}

		public static string GetTypeTemplateMenu(int type)
		{
			switch (type)
			{
				case 1:
				{
					return FormUI.News;
				}
				case 2:
				{
					return FormUI.Product;
				}
				case 3:
				{
					return TemplateContent.Contact.ToString();
				}
				case 4:
				{
					return TemplateContent.AboutUs.ToString();
				}
				case 5:
				{
					return TemplateContent.Static.ToString();
				}
				case 6:
				{
					return TemplateContent.FixItem.ToString();
				}
				case 7:
				{
					return TemplateContent.SaleOff.ToString();
				}
			}
			return string.Empty;
		}
	}
}