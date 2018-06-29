using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.Brandes
{
    public class BrandViewModel
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

		[Display(Name= "Name", ResourceType=typeof(FormUI))]
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

		[Display(Name="Status", ResourceType=typeof(FormUI))]
		public int Status
		{
			get;
			set;
		}
	}
}