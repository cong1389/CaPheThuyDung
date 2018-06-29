using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.GenericControl
{
	public class GenericControlValueViewModel
	{
	    [Display(Name = "GenericControl", ResourceType = typeof(FormUI))]
        public int GenericControlId
		{
			get;
			set;
		}

		[Display(Name="ColorHex", ResourceType=typeof(FormUI))]
		public string ColorHex
		{
			get;
			set;
		}

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

		[Display(Name="OrderDisplay", ResourceType=typeof(FormUI))]
		public int? OrderDisplay
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

		[Display(Name="ValueName", ResourceType=typeof(FormUI))]
		public string ValueName
		{
			get;
			set;
		}

        public int EntityId
        {
            get;
            set;
        }
	}
}