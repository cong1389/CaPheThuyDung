using App.Core.Common;
using App.Domain.Entities.Menu;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Data
{
	public class News : AuditableEntity<int>
	{
		public string Description
		{
			get;
			set;
		}

		public bool HomeDisplay
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

		public int MenuId
		{
			get;
			set;
		}

		[ForeignKey("MenuId")]
		public virtual MenuLink MenuLink
		{
			get;
			set;
		}

		public string MetaDescription
		{
			get;
			set;
		}

		public string MetaKeywords
		{
			get;
			set;
		}

		public string MetaTitle
		{
			get;
			set;
		}

		public int OrderDisplay
		{
			get;
			set;
		}

		public string OtherLink
		{
			get;
			set;
		}

		public string SeoUrl
		{
			get;
			set;
		}

		public string ShortDesc
		{
			get;
			set;
		}

		public bool SpecialDisplay
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

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

		public string VideoLink
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

		public string VirtualCatUrl
		{
			get;
			set;
		}

		public News()
		{
		}
	}
}