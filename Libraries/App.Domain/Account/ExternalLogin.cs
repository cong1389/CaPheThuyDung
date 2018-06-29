using System;
using App.Core.Common;

namespace App.Domain.Entities.Account
{
	public class ExternalLogin : BaseEntity
	{
		private User _user;

		public virtual string LoginProvider
		{
			get;
			set;
		}

		public virtual string ProviderKey
		{
			get;
			set;
		}

		public virtual User User
		{
			get => _user;
		    set
			{
				_user = value;
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