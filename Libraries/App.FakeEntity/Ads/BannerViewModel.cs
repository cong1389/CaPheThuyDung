using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using App.FakeEntity.Menu;
using Resources;

namespace App.FakeEntity.Ads
{
	public class BannerViewModel
	{
		[Display(Name="FromDate", ResourceType=typeof(FormUI))]
		public TimeSpan? FromDate
		{
			get;
			set;
		}

		[Display(Name="Height", ResourceType=typeof(FormUI))]
		public string Height
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		[Display(Name= "Image", ResourceType=typeof(FormUI))]
		public HttpPostedFileBase Image
		{
			get;
			set;
		}

		[Display(Name="ImageUrl", ResourceType=typeof(FormUI))]
		public string ImgPath
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		[Display(Name="MenuLink", ResourceType=typeof(FormUI))]
		public int? MenuId
		{
			get;
			set;
		}

		public MenuLinkViewModel MenuLink
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

		public PageBannerViewModel PageBanner
		{
			get;
			set;
		}

		[Display(Name="PageBanner", ResourceType=typeof(FormUI))]
		public int PageId
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

		[Display(Name="Target", ResourceType=typeof(FormUI))]
		public string Target
		{
			get;
			set;
		}

		[Display(Name= "Title", ResourceType=typeof(FormUI))]
		public string Title
		{
			get;
			set;
		}

		[Display(Name="ToDate", ResourceType=typeof(FormUI))]
		public TimeSpan? ToDate
		{
			get;
			set;
		}

		[Display(Name="WebsiteLink", ResourceType=typeof(FormUI))]
		public string WebsiteLink
		{
			get;
			set;
		}

		[Display(Name="Width", ResourceType=typeof(FormUI))]
		public string Width
		{
			get;
			set;
		}
	}
}