using App.Core.Common;
using App.Domain.Entities.Location;
using App.Domain.Entities.Other;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.GlobalSetting
{
    public class ContactInformation : AuditableEntity<int>
    {
        [StringLength(250)]
        public string Address
        {
            get;
            set;
        }

        [StringLength(50)]
        public string Email
        {
            get;
            set;
        }

        [StringLength(50)]
        public string Fax
        {
            get;
            set;
        }

        [StringLength(50)]
        public string Hotline
        {
            get;
            set;
        }

        [StringLength(50)]
        public string Lag
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

        [StringLength(50)]
        public string Lat
        {
            get;
            set;
        }

        [StringLength(50)]
        public string MobilePhone
        {
            get;
            set;
        }

        public string NumberOfStore
        {
            get;
            set;
        }

        public int OrderDisplay
        {
            get;
            set;
        }

        [ForeignKey("ProvinceId")]
        public virtual Province Province
        {
            get;
            set;
        }

        public int? ProvinceId
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

        public int Type
        {
            get;
            set;
        }

        public ContactInformation()
        {
        }

        public virtual ICollection<LandingPage> LandingPages
        {
            get;
            set;
        }

        //private ICollection<App.Domain.Entities.GenericControl.GenericControl> _genericControls;
        //public virtual ICollection<GenericControl.GenericControl> GenericControls
        //{
        //    get
        //    {
        //        ICollection<GenericControl.GenericControl> genericControls = this._genericControls;
        //        if (genericControls == null)
        //        {
        //            List<GenericControl.GenericControl> genericControl1 = new List<GenericControl.GenericControl>();
        //            ICollection<GenericControl.GenericControl> genericControl2 = genericControl1;
        //            this._genericControls = genericControl1;
        //            genericControls = genericControl2;
        //        }
        //        return genericControls;
        //    }
        //    set
        //    {
        //        this._genericControls = value;
        //    }
        //}

        //private ICollection<GenericControlValue> genericControlValues;

        //public virtual ICollection<GenericControlValue> GenericControlValues
        //{
        //    get
        //    {
        //        ICollection<GenericControlValue> GenericControlValues = this.genericControlValues;
        //        if (GenericControlValues == null)
        //        {
        //            List<GenericControlValue> GenericControlValues1 = new List<GenericControlValue>();
        //            ICollection<GenericControlValue> GenericControlValues2 = GenericControlValues1;
        //            this.genericControlValues = GenericControlValues1;
        //            GenericControlValues = GenericControlValues2;
        //        }
        //        return GenericControlValues;
        //    }
        //    set
        //    {
        //        this.genericControlValues = value;
        //    }
        //}
    }
}