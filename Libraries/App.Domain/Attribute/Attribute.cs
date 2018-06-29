using App.Core.Common;
using System.Collections.Generic;

namespace App.Domain.Entities.Attribute
{
	public class Attribute : AuditableEntity<int>
	{
		public string AttributeName
		{
			get;
			set;
		}

		public virtual ICollection<AttributeValue> AttributeValues
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public int? OrderDisplay
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

		public Attribute()
		{
		}
	}
}