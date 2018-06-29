using System;

namespace App.Core.Common
{
    public interface IAuditableEntity
	{
		string CreatedBy
		{
			get;
			set;
		}

		DateTime CreatedDate
		{
			get;
			set;
		}

		string UpdatedBy
		{
			get;
			set;
		}

		DateTime? UpdatedDate
		{
			get;
			set;
		}
	}
}