using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.Ads
{
	public class PageBannerViewModel
	{
		public int Id
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		[Display(Name="OrderDisplay", ResourceType=typeof(FormUI))]
		public int OrderDisplay
		{
			get;
			set;
		}

		[Display(Name="PageName", ResourceType=typeof(FormUI))]
		public string PageName
		{
			get;
			set;
		}

		[Display(Name="Position", ResourceType=typeof(FormUI))]
		public int Position
		{
			get;
			set;
		}

		[Display(Name="Status", ResourceType=typeof(FormUI))]
		public int Status
		{
			get;
			set;
		}
	}
}