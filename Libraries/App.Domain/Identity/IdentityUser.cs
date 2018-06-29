using Microsoft.AspNet.Identity;
using System;

namespace App.Domain.Entities.Identity
{
	public class IdentityUser : IUser<Guid>
	{
		public virtual string Address
		{
			get;
			set;
		}

		public virtual string City
		{
			get;
			set;
		}

		public DateTime Created
		{
			get;
			set;
		}

		public virtual string Email
		{
			get;
			set;
		}

	    public virtual bool EmailConfirmed { get; set; }

        public virtual string FirstName
		{
			get;
			set;
		}

		public Guid Id
		{
			get => JustDecompileGenerated_get_Id();
		    set => JustDecompileGenerated_set_Id(value);
		}

		private Guid _justDecompileGeneratedIdKBackingField;

		public Guid JustDecompileGenerated_get_Id()
		{
			return _justDecompileGeneratedIdKBackingField;
		}

		public void JustDecompileGenerated_set_Id(Guid value)
		{
			_justDecompileGeneratedIdKBackingField = value;
		}

		public bool IsLockedOut
		{
			get;
			set;
		}

		public bool IsSuperAdmin
		{
			get;
			set;
		}

		public virtual string LastName
		{
			get;
			set;
		}

		public virtual string MiddleName
		{
			get;
			set;
		}

		public virtual string PasswordHash
		{
			get;
			set;
		}

		public virtual string Phone
		{
			get;
			set;
		}

		public virtual string SecurityStamp
		{
			get;
			set;
		}

		public virtual string State
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public IdentityUser()
		{
			Id = Guid.NewGuid();
		}

		public IdentityUser(string userName, string email, string firstName, string lastName, string middleName, string phone,
		    string addess, string city, string state, bool superAdmin, bool isLockedOut, DateTime createdDate) : this()
		{
			UserName = userName;
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			MiddleName = middleName;
			Phone = phone;
			Address = addess;
			City = city;
			State = state;
			IsSuperAdmin = superAdmin;
			Created = createdDate;
			IsLockedOut = isLockedOut;
		}
	}
}