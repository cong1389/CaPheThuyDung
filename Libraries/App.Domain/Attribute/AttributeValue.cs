using App.Core.Common;
using App.Domain.Entities.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Attribute
{
	public class AttributeValue : AuditableEntity<int>
	{
		private ICollection<Post> _posts;

		[ForeignKey("AttributeId")]
		public virtual Attribute Attribute
		{
			get;
			set;
		}

		public int AttributeId
		{
			get;
			set;
		}

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

		public virtual ICollection<GalleryImage> GalleryImages
		{
			get;
			set;
		}

		public int? OrderDisplay
		{
			get;
			set;
		}

		public ICollection<Post> Posts
		{
			get
			{
				ICollection<Post> posts = _posts;
				if (posts == null)
				{
					List<Post> posts1 = new List<Post>();
					ICollection<Post> posts2 = posts1;
					_posts = posts1;
					posts = posts2;
				}
				return posts;
			}
			set => _posts = value;
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

		public AttributeValue()
		{
		}
	}
}