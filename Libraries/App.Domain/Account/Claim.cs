using System;
using App.Core.Common;

namespace App.Domain.Entities.Account
{
	public class Claim : Entity<int>
	{
		private User _user;

		public virtual string ClaimType
		{
			get;
			set;
		}

		public virtual string ClaimValue
		{
			get;
			set;
		}

		public virtual User User
		{
			get => _user;
		    set
			{
                _user = value ?? throw new ArgumentNullException("value");
				UserId = value.Id;
			}
		}

		public virtual Guid UserId
		{
			get;
			set;
		}
	}
}