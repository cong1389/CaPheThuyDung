using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.Location
{
	public class DistrictViewModel
	{
		[Display(Name="Description", ResourceType=typeof(FormUI))]
		public string Description
		{
			get;
			set;
		}

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

		[Display(Name= "Title", ResourceType=typeof(FormUI))]
		public string Name
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

		public ProvinceViewModel Province
		{
			get;
			set;
		}

		[Display(Name="Provinces", ResourceType=typeof(FormUI))]
		public int ProvinceId
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