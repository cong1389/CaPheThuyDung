using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.Attribute
{
	public class AttributeValueViewModel
	{
		public AttributeViewModel Attribute
		{
			get;
			set;
		}

		[Display(Name="AttributeName", ResourceType=typeof(FormUI))]
		public int AttributeId
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
	}
}