using System.Configuration;

namespace App.Front.Models
{
	public static class ImageSize
	{
		public static int HeightBigSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightBigSize"]))
				{
					return 1160;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeightBigSize"]);
			}
		}

		public static int HeighthOrignalSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeighthOrignalSize"]))
				{
					return 1160;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeighthOrignalSize"]);
			}
		}

		public static int HeightMediumSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightMediumSize"]))
				{
					return 547;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeightMediumSize"]);
			}
		}

		public static int HeightNewsSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightNewsSize"]))
				{
					return 200;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeightNewsSize"]);
			}
		}

		public static int HeightPostRelativeSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightPostRelativeSize"]))
				{
					return 384;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeightPostRelativeSize"]);
			}
		}

		public static int HeightSmallSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightSmallSize"]))
				{
					return 70;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeightSmallSize"]);
			}
		}

		public static int HeightThumbnailSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightThumbnailSize"]))
				{
					return 280;
				}
				return int.Parse(ConfigurationManager.AppSettings["HeightThumbnailSize"]);
			}
		}

		public static int WithBigSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithBigSize"]))
				{
					return 840;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithBigSize"]);
			}
		}

		public static int WithMediumSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithMediumSize"]))
				{
					return 380;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithMediumSize"]);
			}
		}

		public static int WithNewsSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithNewsSize"]))
				{
					return 530;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithNewsSize"]);
			}
		}

		public static int WithOrignalSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithOrignalSize"]))
				{
					return 840;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithOrignalSize"]);
			}
		}

		public static int WithPostRelativeSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithPostRelativeSize"]))
				{
					return 380;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithPostRelativeSize"]);
			}
		}

		public static int WithSmallSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithSmallSize"]))
				{
					return 105;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithSmallSize"]);
			}
		}

		public static int WithThumbnailSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WithThumbnailSize"]))
				{
					return 200;
				}
				return int.Parse(ConfigurationManager.AppSettings["WithThumbnailSize"]);
			}
		}
	}
}