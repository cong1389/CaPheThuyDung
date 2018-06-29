using System.Configuration;

namespace App.Admin.Helpers
{
    public static class ImageSize
	{
		public static int WidthDefaultSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["WidthDefaultSize"]))
				{
					return int.Parse(ConfigurationManager.AppSettings["WidthDefaultSize"]);
				}
				return int.Parse(ConfigurationManager.AppSettings["WidthDefaultSize"]);
			}
		}

		public static int HeightDefaultSize
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["HeightDefaultSize"]))
				{
                    return int.Parse(ConfigurationManager.AppSettings["HeightDefaultSize"]);
                }
				return int.Parse(ConfigurationManager.AppSettings["HeightDefaultSize"]);
			}
		}

		
    }
}