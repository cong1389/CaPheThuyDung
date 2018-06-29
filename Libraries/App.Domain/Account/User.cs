using System;
using System.Collections.Generic;
using App.Core.Common;

namespace App.Domain.Entities.Account
{
	public class User : Entity<Guid>
	{
		private ICollection<Claim> _claims;

		private ICollection<ExternalLogin> _externalLogins;

		private ICollection<Role> _roles;

		public string Address
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public virtual ICollection<Claim> Claims
		{
			get
			{
				ICollection<Claim> claims = _claims;

			    if (claims != null) return claims;

			    List<Claim> claims1 = new List<Claim>();
			    ICollection<Claim> claims2 = claims1;
			    _claims = claims1;
			    claims = claims2;
			    return claims;
			}
			set => _claims = value;
		}

		public DateTime Created
		{
			get;
			set;
		}

		public string DisplayAddress
		{
			get
			{
				string str = string.IsNullOrEmpty(Address) ? string.Empty : Address;
				string str1 = string.IsNullOrEmpty(City) ? string.Empty : City;
				return $"{str} {str1} {(string.IsNullOrEmpty(State) ? string.Empty : State)}";
			}
		}

		public string DisplayName
		{
			get
			{
				string str = string.IsNullOrEmpty(FirstName) ? string.Empty : FirstName;
				string str1 = string.IsNullOrEmpty(MiddleName) ? string.Empty : MiddleName;
				return $"{(string.IsNullOrEmpty(LastName) ? string.Empty : LastName)} {str1} {str}";
			}
		}

		public string Email
		{
			get;
			set;
		}

	    public bool EmailConfirmed
        {
	        get;
	        set;
	    }

        public string FirstName
		{
			get;
			set;
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

		public DateTime? LastLogin
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public virtual ICollection<ExternalLogin> Logins
		{
			get
			{
				ICollection<ExternalLogin> externalLogins = _externalLogins;
				if (externalLogins == null)
				{
					List<ExternalLogin> externalLogins1 = new List<ExternalLogin>();
					ICollection<ExternalLogin> externalLogins2 = externalLogins1;
					_externalLogins = externalLogins1;
					externalLogins = externalLogins2;
				}
				return externalLogins;
			}
			set => _externalLogins = value;
		}

		public string MiddleName
		{
			get;
			set;
		}

		public virtual string PasswordHash
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public virtual ICollection<Role> Roles
		{
			get
			{
				ICollection<Role> roles = _roles;
				if (roles == null)
				{
					List<Role> roles1 = new List<Role>();
					ICollection<Role> roles2 = roles1;
					_roles = roles1;
					roles = roles2;
				}
				return roles;
			}
			set => _roles = value;
		}

		public virtual string SecurityStamp
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}
        
        public string UserName
		{
			get;
			set;
		}
	}
}