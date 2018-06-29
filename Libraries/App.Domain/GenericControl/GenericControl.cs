using App.Core.Common;
using App.Domain.Entities.Menu;
using System.Collections.Generic;

namespace App.Domain.Entities.GenericControl
{
	public class GenericControl : AuditableEntity<int>
	{
		public string Name
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

        public int MenuId
        {
            get;
            set;
        }        

        public GenericControl()
		{
		}

        //[ForeignKey("EntityId")]
        //public virtual ContactInformation ContactInfo
        //{
        //    get;
        //    set;
        //}

        public int? ControlTypeId { get; set; }

        public virtual ICollection<GenericControlValue> GenericControlValues
        {
            get;
            set;
        }

        //public ICollection<MenuLink> _menuLinks;

        //public ICollection<MenuLink> MenuLinks
        //{
        //    get
        //    {
        //        return _menuLinks ?? (_menuLinks = new HashSet<MenuLink>());
        //    }
        //    set
        //    {
        //        this._menuLinks = value;
        //    }
        //}

        public virtual ICollection<MenuLink> MenuLinks
        {
            get;
            set;
        }
    }
}