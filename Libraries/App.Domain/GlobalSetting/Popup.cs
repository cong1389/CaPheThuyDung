using App.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.GlobalSetting
{
	public class Popup : AuditableEntity<int>
	{
		[Column(TypeName="ntext")]
		public string Description
		{
			get;
			set;
		}

		public DateTime? FromDate
		{
			get;
			set;
		}

		[StringLength(450)]
		public string ImagePath
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

		public int Status
		{
			get;
			set;
		}

		[StringLength(450)]
		public string Title
		{
			get;
			set;
		}

		public DateTime? ToDate
		{
			get;
			set;
		}

		public Popup()
		{
		}
	}
}