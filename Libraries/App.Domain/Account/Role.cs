using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Core.Common;

namespace App.Domain.Entities.Account
{
	public class Role : Entity<Guid>
	{
		private ICollection<User> _users;

		[StringLength(450)]
		public string Description
		{
			get;
			set;
		}

		[StringLength(250)]
		public string Name
		{
			get;
			set;
		}

		public ICollection<User> Users
		{
			get
			{
				var users = _users;

			    if (users != null) return users;

			    List<User> users1 = new List<User>();
			    ICollection<User> users2 = users1;
			    _users = users1;
			    users = users2;
			    return users;
			}
			set => _users = value;
		}
	}
}