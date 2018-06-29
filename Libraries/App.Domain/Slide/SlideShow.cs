using App.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Slide
{
	public class SlideShow : AuditableEntity<int>
	{
		public string Description
		{
			get;
			set;
		}

		public TimeSpan? FromDate
		{
			get;
			set;
		}

		[StringLength(50)]
		public string Height
		{
			get;
			set;
		}

		public string ImgPath
		{
			get;
			set;
		}

		public int OrderDisplay
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		[StringLength(50)]
		public string Target
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public TimeSpan? ToDate
		{
			get;
			set;
		}

		public bool Video
		{
			get;
			set;
		}

		public string WebsiteLink
		{
			get;
			set;
		}

		[StringLength(50)]
		public string Width
		{
			get;
			set;
		}

		public SlideShow()
		{
		}
	}
}