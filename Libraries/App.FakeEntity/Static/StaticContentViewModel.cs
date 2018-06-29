using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.FakeEntity.Menu;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.Static
{
	public class StaticContentViewModel : ILocalizedModel<StaticContentLocalesViewModel>
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

		[Display(Name="Image", ResourceType=typeof(FormUI))]
		public HttpPostedFileBase Image
		{
			get;
			set;
		}

		[Display(Name= "ImageUrl", ResourceType=typeof(FormUI))]
		public string ImagePath
		{
			get;
			set;
		}

		public string Language
		{
			get;
			set;
		}

		[Display(Name="MenuLink", ResourceType=typeof(FormUI))]
		public int MenuId
		{
			get;
			set;
		}

		public MenuLinkViewModel MenuLink
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

		[Display(Name="SeoUrl", ResourceType=typeof(FormUI))]
		public string SeoUrl
		{
			get;
			set;
		}

        [AllowHtml]
		[Display(Name="ShortDesc", ResourceType=typeof(FormUI))]
		public string ShortDesc
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

		public int ViewCount
		{
			get;
			set;
		}
        public string VirtualCategoryId
        {
            get;
            set;
        }

        public IList<StaticContentLocalesViewModel> Locales { get; set; }

        public StaticContentViewModel()
		{
            Locales = new List<StaticContentLocalesViewModel>();
        }
	}

    public class StaticContentLocalesViewModel : ILocalizedModelLocal
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

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Image", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase Image
        {
            get;
            set;
        }

        [Display(Name = "ImageUrl", ResourceType = typeof(FormUI))]
        public string ImagePath
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        [Display(Name = "MenuLink", ResourceType = typeof(FormUI))]
        public int MenuId
        {
            get;
            set;
        }

        public MenuLinkViewModel MenuLink
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

        [Display(Name = "SeoUrl", ResourceType = typeof(FormUI))]
        public string SeoUrl
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "ShortDesc", ResourceType = typeof(FormUI))]
        public string ShortDesc
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

        public int ViewCount
        {
            get;
            set;
        }
        public string VirtualCategoryId
        {
            get;
            set;
        }
    }
}