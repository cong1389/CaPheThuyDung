using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace App.FakeEntity.Manufacturers
{
	public class ManufacturerViewModel
	{
        [AllowHtml]
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

		[Display(Name= "Image", ResourceType=typeof(FormUI))]
		public HttpPostedFileBase Image
		{
			get;
			set;
		}

		public string ImageUrl
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

		[Display(Name="SourceLink", ResourceType=typeof(FormUI))]
		public string OtherLink
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

		[Display(Name="Title", ResourceType=typeof(FormUI))]
		public string Title
		{
			get;
			set;
		}
	}
}