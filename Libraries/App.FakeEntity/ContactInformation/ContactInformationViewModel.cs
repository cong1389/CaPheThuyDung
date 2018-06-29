using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.FakeEntity.Location;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.ContactInformation
{
	public class ContactInformationViewModel : ILocalizedModel<ContactInformationLocalesViewModel>
    {
		[Display(Name="Address", ResourceType=typeof(FormUI))]
		public string Address
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

		[Display(Name="Fax", ResourceType=typeof(FormUI))]
		public string Fax
		{
			get;
			set;
		}

		[Display(Name="Hotline", ResourceType=typeof(FormUI))]
		public string Hotline
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		[Display(Name="Lag", ResourceType=typeof(FormUI))]
		public string Lag
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		[Display(Name="Lat", ResourceType=typeof(FormUI))]
		public string Lat
		{
			get;
			set;
		}

		[Display(Name="MobilePhone", ResourceType=typeof(FormUI))]
		public string MobilePhone
		{
			get;
			set;
		}

		[Display(Name="NumberOfStore", ResourceType=typeof(FormUI))]
		public string NumberOfStore
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

		[Display(Name= "Title", ResourceType=typeof(FormUI))]
		public string Title
		{
			get;
			set;
		}

		[Display(Name="ContactType", ResourceType=typeof(FormUI))]
		public int Type
		{
			get;
			set;
		}

        public List<Domain.Entities.GenericControl.GenericControl> GenericControls
        {
            get;
            set;
        }       

        public IList<ContactInformationLocalesViewModel> Locales { get; set; }

        public ContactInformationViewModel()
		{
            GenericControls = new List<Domain.Entities.GenericControl.GenericControl>();
            Locales = new List<ContactInformationLocalesViewModel>();
        }
	}

    public class ContactInformationLocalesViewModel: ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        public int LocalesId { get; set; }

        [Display(Name = "Address", ResourceType = typeof(FormUI))]
        public string Address
        {
            get;
            set;
        }

        [Display(Name = "Email", ResourceType = typeof(FormUI))]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "Fax", ResourceType = typeof(FormUI))]
        public string Fax
        {
            get;
            set;
        }

        [Display(Name = "Hotline", ResourceType = typeof(FormUI))]
        public string Hotline
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Lag", ResourceType = typeof(FormUI))]
        public string Lag
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        [Display(Name = "Lat", ResourceType = typeof(FormUI))]
        public string Lat
        {
            get;
            set;
        }

        [Display(Name = "MobilePhone", ResourceType = typeof(FormUI))]
        public string MobilePhone
        {
            get;
            set;
        }

        [Display(Name = "NumberOfStore", ResourceType = typeof(FormUI))]
        public string NumberOfStore
        {
            get;
            set;
        }

        [Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
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

        [Display(Name = "Provinces", ResourceType = typeof(FormUI))]
        public int ProvinceId
        {
            get;
            set;
        }

        [Display(Name = "Status", ResourceType = typeof(FormUI))]
        public int Status
        {
            get;
            set;
        }

        [Display(Name = "Title", ResourceType = typeof(FormUI))]
        public string Title
        {
            get;
            set;
        }

        [Display(Name = "ContactType", ResourceType = typeof(FormUI))]
        public int Type
        {
            get;
            set;
        }

        public List<Domain.Entities.GenericControl.GenericControl> GenericControls
        {
            get;
            set;
        }
    }
}