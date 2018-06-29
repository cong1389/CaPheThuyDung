using App.Core.Common;
using App.Domain.Entities.Attribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Data
{
    public class GalleryImage : AuditableEntity<int>
    {
        [ForeignKey("AttributeValueId")]
        public virtual AttributeValue AttributeValue
        {
            get;
            set;
        }

        public int AttributeValueId
        {
            get;
            set;
        }

        public string ImageBig
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

        [ForeignKey("PostId")]
        public virtual Post Post
        {
            get;
            set;
        }

        public int PostId
        {
            get;
            set;
        }

        public decimal? Price
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

        public GalleryImage()
        {
        }
    }
}