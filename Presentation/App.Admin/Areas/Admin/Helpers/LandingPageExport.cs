using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace App.Admin.Helpers
{
	public class LandingPageExport
	{
		[Display(Name="Ngày sinh")]
		public string DateOfBith
		{
			get;
			set;
		}

		[Display(Name="Email")]
		public string Email
		{
			get;
			set;
		}

		[Display(Name="Họ tên")]
		public string FullName
		{
			get;
			set;
		}

		[Display(Name="Điện thoại")]
		public string PhoneNumber
		{
			get;
			set;
		}

		[Display(Name="Địa chỉ nhận quà")]
		public string PlaceOfGift
		{
			get;
			set;
		}

		[Display(Name="Trạng thái")]
		public string Status
		{
			get;
			set;
		}

		public LandingPageExport()
		{
		}
	}
}