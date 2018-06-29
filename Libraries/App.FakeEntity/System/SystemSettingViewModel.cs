using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.System
{
	public class SystemSettingViewModel : ILocalizedModel<SystemSettingLocalesViewModel>
    {
        [AllowHtml]
        [Display(Name="Description", ResourceType=typeof(FormUI))]
		public string Description
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}
        
		[AllowHtml]
		[Display(Name="FooterContent", ResourceType=typeof(FormUI))]
		public string FooterContent
		{
			get;
			set;
		}

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

		public string Language
		{
			get;
			set;
		}
                
        public string FaviconImage
        {
            get;
            set;
        }

        [Display(Name = "Favicon", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase Favicon
        {
            get;
            set;
        }

        [Display(Name="LogoImage", ResourceType=typeof(FormUI))]
		public HttpPostedFileBase Logo
		{
			get;
			set;
		}

		public string LogoImage
		{
			get;
			set;
		}

        [Display(Name = "LogoFooter", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase LogoFooter
        {
            get;
            set;
        }

        public string LogoFooterImage
        {
            get;
            set;
        }

        [Display(Name="MaintanceSite", ResourceType=typeof(FormUI))]
		public bool MaintanceSite
		{
			get;
			set;
		}

		[Display(Name="MetaDescription", ResourceType=typeof(FormUI))]
		public string MetaDescription
		{
			get;
			set;
		}

		[Display(Name="MetaKeywords", ResourceType=typeof(FormUI))]
		public string MetaKeywords
		{
			get;
			set;
		}

		[Display(Name="MetaTitle", ResourceType=typeof(FormUI))]
		public string MetaTitle
		{
			get;
			set;
		}

		[Display(Name="Slogan")]
		public string Slogan
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

		[Display(Name="TimeWork", ResourceType=typeof(FormUI))]
		public string TimeWork
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

        public IList<SystemSettingLocalesViewModel> Locales { get; set; }

        public SystemSettingViewModel()
		{
            Locales = new List<SystemSettingLocalesViewModel>();
        }
	}

    public class SystemSettingLocalesViewModel :  ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        public int LocalesId { get; set; }

        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(FormUI))]
        public string Description
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string FaviconImage
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "FooterContent", ResourceType = typeof(FormUI))]
        public string FooterContent
        {
            get;
            set;
        }

        public string Hotline
        {
            get;
            set;
        }

        [Display(Name = "Favicon", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase Favicon
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

        [Display(Name = "LogoImage", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase Logo
        {
            get;
            set;
        }

        public string LogoImage
        {
            get;
            set;
        }

        [Display(Name = "LogoFooter", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase LogoFooter
        {
            get;
            set;
        }

        public string LogoFooterImage
        {
            get;
            set;
        }

        [Display(Name = "MaintanceSite", ResourceType = typeof(FormUI))]
        public bool MaintanceSite
        {
            get;
            set;
        }

        [Display(Name = "MetaDescription", ResourceType = typeof(FormUI))]
        public string MetaDescription
        {
            get;
            set;
        }

        [Display(Name = "MetaKeywords", ResourceType = typeof(FormUI))]
        public string MetaKeywords
        {
            get;
            set;
        }

        [Display(Name = "MetaTitle", ResourceType = typeof(FormUI))]
        public string MetaTitle
        {
            get;
            set;
        }

        [Display(Name = "Slogan")]
        public string Slogan
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

        [Display(Name = "TimeWork", ResourceType = typeof(FormUI))]
        public string TimeWork
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
        
    }
}