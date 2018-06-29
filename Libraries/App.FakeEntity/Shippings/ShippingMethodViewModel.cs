using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.Shippings
{
    public class ShippingMethodViewModel : ILocalizedModel<ShippingMethodLocalesViewModel>
	{
		public int Id { get; set; }

        public string Name
        {
            get; set;
        }

		[AllowHtml]
		[Display(Name = "Description", ResourceType = typeof(FormUI))]
		public string Description
        {
            get; set;
        }

		[Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
		public int OrderDisplay
        {
            get; set;
        }

        public bool IgnoreCharges
        {
            get; set;
        }

		[Display(Name = "Status", ResourceType = typeof(FormUI))]
		public int Status
		{
			get;
			set;
		}

		public IList<ShippingMethodLocalesViewModel> Locales { get; set; }

		public ShippingMethodViewModel()
	    {
		    Locales = new List<ShippingMethodLocalesViewModel>();
	    }
	}

	public class ShippingMethodLocalesViewModel : ILocalizedModelLocal
	{
		public int LanguageId { get; set; }

		public int LocalesId { get; set; }

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get; set;
		}

		[AllowHtml]
		[Display(Name = "Description", ResourceType = typeof(FormUI))]
		public string Description
		{
			get; set;
		}

		[Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
		public int OrderDisplay
		{
			get; set;
		}
		public bool IgnoreCharges
		{
			get; set;
		}

		[Display(Name = "Status", ResourceType = typeof(FormUI))]
		public int Status
		{
			get;
			set;
		}
	}
}
