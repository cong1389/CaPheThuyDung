using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using App.Domain.Entities.Data;
using App.FakeEntity.Attribute;
using App.FakeEntity.Gallery;
using App.FakeEntity.Menu;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.Post
{
    public class PostViewModel: ILocalizedModel<PostLocalesViewModel>
    {
		public List<AttributeValueViewModel> AttributeValues
		{
			get;
			set;
		}

		[AllowHtml]
		[Display(Name="Description", ResourceType=typeof(FormUI))]
		public string Description
		{
			get;
			set;
		}

		[Display(Name="Discount", ResourceType=typeof(FormUI))]
		public double? Discount
		{
			get;
			set;
		}

		[DataType(DataType.Date)]
		[Display(Name="ToDate", ResourceType=typeof(FormUI))]
		[DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:MM/dd/yyyy}")]
		public DateTime? EndDate
		{
			get;
			set;
		}

		public List<GalleryImageViewModel> GalleryImages
		{
			get;
			set;
		}

        public List<PostGalleryViewModel> PostGallerys
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
		public int? MenuId
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

		[Display(Name="OldOrNew", ResourceType=typeof(FormUI))]
		public bool OldOrNew
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

		[Display(Name="OutOfStock", ResourceType=typeof(FormUI))]
		public bool OutOfStock
		{
			get;
			set;
		}

		[Display(Name="PostType", ResourceType=typeof(FormUI))]
		public int PostType
		{
			get;
			set;
		}

		[Display(Name="Price", ResourceType=typeof(FormUI))]
		public double? Price
		{
			get;
			set;
		}

		[Display(Name="ProductCode", ResourceType=typeof(FormUI))]
		public string ProductCode
		{
			get;
			set;
		}

        [Display(Name = "ShowOnHomePage", ResourceType = typeof(FormUI))]
        public bool ShowOnHomePage
        {
            get;
            set;
        }

        [Display(Name="ProductHot", ResourceType=typeof(FormUI))]
		public bool ProductHot
		{
			get;
			set;
		}

		[Display(Name="ProductNew", ResourceType=typeof(FormUI))]
		public bool ProductNew
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

		[DataType(DataType.Date)]
		[Display(Name="FromDate", ResourceType=typeof(FormUI))]
		[DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:MM/dd/yyyy}")]
		public DateTime? StartDate
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

		[AllowHtml]
		[Display(Name="Thông số kỹ thuật")]
		public string TechInfo
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

        [Display(Name = "Manufacturer", ResourceType = typeof(FormUI))]
        public int ManufacturerId
        {
            get;
            set;
        }

        [DataMember]
        public virtual Manufacturer Manufacturer { get; set; }

        public IList<PostLocalesViewModel> Locales { get; set; }

        public PostViewModel()
		{
			GalleryImages = new List<GalleryImageViewModel>();
            PostGallerys = new List<PostGalleryViewModel>();
            Locales = new List<PostLocalesViewModel>();
        }
	}

    public class PostLocalesViewModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        public int LocalesId { get; set; }

        public List<AttributeValueViewModel> AttributeValues
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(FormUI))]
        public string Description
        {
            get;
            set;
        }

        [Display(Name = "Discount", ResourceType = typeof(FormUI))]
        public double? Discount
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [Display(Name = "ToDate", ResourceType = typeof(FormUI))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EndDate
        {
            get;
            set;
        }

        public List<GalleryImageViewModel> GalleryImages
        {
            get;
            set;
        }

        public List<PostGalleryViewModel> PostGallerys
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
        public int? MenuId
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

        [Display(Name = "OldOrNew", ResourceType = typeof(FormUI))]
        public bool OldOrNew
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

        [Display(Name = "OutOfStock", ResourceType = typeof(FormUI))]
        public bool OutOfStock
        {
            get;
            set;
        }

        [Display(Name = "PostType", ResourceType = typeof(FormUI))]
        public int PostType
        {
            get;
            set;
        }

        [Display(Name = "Price", ResourceType = typeof(FormUI))]
        public double? Price
        {
            get;
            set;
        }

        [Display(Name = "ProductCode", ResourceType = typeof(FormUI))]
        public string ProductCode
        {
            get;
            set;
        }

        [Display(Name = "ShowOnHomePage", ResourceType = typeof(FormUI))]
        public bool ShowOnHomePage
        {
            get;
            set;
        }

        [Display(Name = "ProductHot", ResourceType = typeof(FormUI))]
        public bool ProductHot
        {
            get;
            set;
        }

        [Display(Name = "ProductNew", ResourceType = typeof(FormUI))]
        public bool ProductNew
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

        [DataType(DataType.Date)]
        [Display(Name = "FromDate", ResourceType = typeof(FormUI))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? StartDate
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

        [AllowHtml]
        [Display(Name = "Thông số kỹ thuật")]
        public string TechInfo
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

        [Display(Name = "Manufacturer", ResourceType = typeof(FormUI))]
        public int ManufacturerId
        {
            get;
            set;
        }

        [DataMember]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}