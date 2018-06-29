using System.ComponentModel.DataAnnotations;
using App.FakeEntity.ContactInformation;
using Resources;

namespace App.FakeEntity.Other
{
	public class LandingPageViewModel
	{
		public ContactInformationViewModel ContactInformation
		{
			get;
			set;
		}

		[Display(Name="DateOfBith", ResourceType=typeof(FormUI))]
		public string DateOfBith
		{
			get;
			set;
		}

		[Display(Name="Email", ResourceType=typeof(FormUI))]
		public string Email
		{
			get;
			set;
		}

		[Display(Name="FullName", ResourceType=typeof(FormUI))]
		public string FullName
		{
			get;
			set;
		}

		[Display(Name="PhoneNumber", ResourceType=typeof(FormUI))]
		public string PhoneNumber
		{
			get;
			set;
		}

		[Display(Name="ShopId", ResourceType=typeof(FormUI))]
		public int ShopId
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