using App.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.GenericControl
{
    public class GenericControlValueItem : AuditableEntity<int>
    {
        [ForeignKey("GenericControlValueId")]
        public virtual GenericControlValue GenericControlValue
        {
            get;
            set;
        }

        public int GenericControlValueId
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public string ImageThumbnail
        {
            get;
            set;
        }

        public int OrderDisplay
        {
            get;
            set;
        }

        //[ForeignKey("PostId")]
        //public virtual App.Domain.Entities.Data.Post Post
        //{
        //    get;
        //    set;
        //}

        public int EntityId
        {
            get;
            set;
        }

        public string Value
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

        public GenericControlValueItem()
        {
        }
    }
}