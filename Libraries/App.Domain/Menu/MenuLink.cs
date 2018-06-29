using System.Collections;
using App.Core.Common;
using App.Domain.Entities.Ads;
using App.Domain.Entities.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Menu
{
    public class MenuLink : AuditableEntity<int>
    {
		public ICollection<Banner> Banners
		{
			get;
			set;
		}

        //public virtual ICollection<App.Domain.Entities.GenericControl.GenericControl> _genericControl
        //{
        //    get;
        //    set;
        //}

        [StringLength(250)]
		public string CurrentVirtualId
		{
			get;
			set;
		}

		public bool DisplayOnHomePage
		{
			get;
			set;
		}

		public bool DisplayOnMenu
		{
			get;
			set;
		}

		public bool DisplayOnSearch
		{
			get;
			set;
		}

        [StringLength(250)]
        public string ImageBigSize
        {
            get;
            set;
        }

        [StringLength(250)]
        public string ImageMediumSize
		{
			get;
			set;
		}

        [StringLength(250)]
        public string ImageSmallSize
		{
			get;
			set;
		}

		[StringLength(5)]
		public string Language
		{
			get;
			set;
		}

		[StringLength(250)]
		public string MenuName
		{
			get;
			set;
		}

		[StringLength(550)]
		public string MetaDescription
		{
			get;
			set;
		}

		[StringLength(550)]
		public string MetaKeywords
		{
			get;
			set;
		}

		[StringLength(550)]
		public string MetaTitle
		{
			get;
			set;
		}

		public ICollection<News> News
		{
			get;
			set;
		}

		public int OrderDisplay
		{
			get;
			set;
		}

		public int? ParentId
		{
			get;
			set;
		}

		public MenuLink ParentMenu
		{
			get;
			set;
		}

		public int Position
		{
			get;
			set;
		}

		public ICollection<Post> Posts
		{
			get;
			set;
		}

		[StringLength(250)]
		public string SeoUrl
		{
			get;
			set;
		}

		[StringLength(250)]
		public string SourceLink
		{
			get;
			set;
		}

		public ICollection<StaticContent> StaticContents
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		public int TemplateType
		{
			get;
			set;
		}

		public int TypeMenu
		{
			get;
			set;
		}

		[StringLength(250)]
		public string VirtualId
		{
			get;
			set;
		}

		[StringLength(250)]
		public string VirtualSeoUrl
		{
			get;
			set;
		}

        public string ColorHex
        {
            get;
            set;
        }

        public virtual ICollection<GenericControl.GenericControl> GenericControls
	    {
	        get; set;
	    }

        public MenuLink()
		{
		    GenericControls = new List<GenericControl.GenericControl>();
        }
	}
}