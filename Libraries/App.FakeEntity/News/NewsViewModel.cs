using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.FakeEntity.Menu;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.News
{
	public class NewsViewModel : ILocalizedModel<NewsLocalesViewModel>
    {
		[AllowHtml]
		[Display(Name="Description", ResourceType=typeof(FormUI))]
		public string Description
		{
			get;
			set;
		}

		[Display(Name="HomeDisplay", ResourceType=typeof(FormUI))]
		public bool HomeDisplay
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

		public string ImageBigSize
		{
			get;
			set;
		}

		public string ImageMediumSize
		{
			get;
			set;
		}

		public string ImageSmallSize
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

		[Display(Name="OrderDisplay", ResourceType=typeof(FormUI))]
		public int OrderDisplay
		{
			get;
			set;
		}

		[Display(Name="WebsiteLink", ResourceType=typeof(FormUI))]
		public string OtherLink
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

		[Display(Name="SpecialDisplay", ResourceType=typeof(FormUI))]
		public bool SpecialDisplay
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

		public bool Video
		{
			get;
			set;
		}

		[Display(Name="VideoLink", ResourceType=typeof(FormUI))]
		public string VideoLink
		{
			get;
			set;
		}

		public string ViewCount
		{
			get;
			set;
		}

		public string VirtualCategoryId
		{
			get;
			set;
		}

		public string VirtualCatUrl
		{
			get;
			set;
		}

        public IList<NewsLocalesViewModel> Locales { get; set; }

        public NewsViewModel()
		{
            Locales = new List<NewsLocalesViewModel>();
        }
	}

    public class NewsLocalesViewModel: ILocalizedModelLocal
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

        [Display(Name = "HomeDisplay", ResourceType = typeof(FormUI))]
        public bool HomeDisplay
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

        public string ImageBigSize
        {
            get;
            set;
        }

        public string ImageMediumSize
        {
            get;
            set;
        }

        public string ImageSmallSize
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

        [Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
        public int OrderDisplay
        {
            get;
            set;
        }

        [Display(Name = "WebsiteLink", ResourceType = typeof(FormUI))]
        public string OtherLink
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

        [Display(Name = "SpecialDisplay", ResourceType = typeof(FormUI))]
        public bool SpecialDisplay
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

        public bool Video
        {
            get;
            set;
        }

        [Display(Name = "VideoLink", ResourceType = typeof(FormUI))]
        public string VideoLink
        {
            get;
            set;
        }

        public string ViewCount
        {
            get;
            set;
        }

        public string VirtualCategoryId
        {
            get;
            set;
        }

        public string VirtualCatUrl
        {
            get;
            set;
        }
    }
}