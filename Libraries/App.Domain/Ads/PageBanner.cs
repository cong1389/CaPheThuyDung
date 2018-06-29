using App.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Ads
{
	public class PageBanner : AuditableEntity<int>
	{
		public virtual ICollection<Banner> Banners
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

		public int OrderDisplay
		{
			get;
			set;
		}

		[StringLength(250)]
		public string PageName
		{
			get;
			set;
		}

		public int Position
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		public PageBanner()
		{
		}
	}
}