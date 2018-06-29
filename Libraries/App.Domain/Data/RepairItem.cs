using App.Core.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Data
{
	public class RepairItem : AuditableEntity<int>
	{
		public string Category
		{
			get;
			set;
		}

		public decimal? FixedFee
		{
			get;
			set;
		}

		[ForeignKey("RepairId")]
		public virtual Repair Repair
		{
			get;
			set;
		}

		public int RepairId
		{
			get;
			set;
		}

		public DateTime? WarrantyFrom
		{
			get;
			set;
		}

		public DateTime? WarrantyTo
		{
			get;
			set;
		}

		public RepairItem()
		{
		}
	}
}