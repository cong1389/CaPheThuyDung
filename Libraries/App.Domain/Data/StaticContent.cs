using App.Core.Common;
using App.Domain.Entities.Menu;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Data
{
    public class StaticContent : AuditableEntity<int>
	{
		public string Description
		{
			get;
			set;
		}

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

		public StaticContent()
		{
		}
	}
}