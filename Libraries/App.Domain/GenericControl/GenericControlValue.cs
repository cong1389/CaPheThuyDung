using App.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.GenericControl
{
    public class GenericControlValue : AuditableEntity<int>
	{
		public string ColorHex
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
        public int EntityId
        {
            get;
            set;
        }

        public int Status
		{
			get;
			set;
		}

		public string ValueName
		{
			get;
			set;
		}

        public int GenericControlId
        {
            get;
            set;
        }

        [ForeignKey("GenericControlId")]
        public virtual GenericControl GenericControl
        {
            get;
            set;
        }

        public virtual ICollection<GenericControlValueItem> GenericControlValueItems
        {
            get;
            set;
        }


        public GenericControlValue()
		{
		}
	}
}