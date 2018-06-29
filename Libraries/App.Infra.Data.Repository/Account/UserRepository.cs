using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Account
{
    public class UserRepository : RepositoryBaseAsync<User>, IUserRepository
	{
		public UserRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public User FindByEmail(string email)
		{
			var user = Get(x => x.Email.Equals(email));
			return user;
		}

		public async Task<User> FindByEmailAsync(string email)
		{
			var userRepository = this;
			var async = await userRepository.GetAsync(x => x.Email.Equals(email));
			return async;
		}

		public Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email)
		{
			var async = GetAsync(cancellationToken, x => x.Email.Equals(email));
			return async;
		}

		public User FindByUserName(string username)
		{
			var user = Get(x => x.UserName.Equals(username));
			return user;
		}

		public async Task<User> FindByUserNameAsync(string username)
		{
			var userRepository = this;
			var async = await userRepository.GetAsync(x => x.UserName.Equals(username));
			return async;
		}

		public async Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username)
		{
			var userRepository = this;
			var cancellationToken1 = cancellationToken;
			var async = await userRepository.GetAsync(cancellationToken1, x => x.UserName.Equals(username));
			return async;
		}

		protected override IOrderedQueryable<User> GetDefaultOrder(IQueryable<User> query)
		{
			var users = 
				from x in query
				orderby x.UserName
				select x;
			return users;
		}

		public async Task<IEnumerable<User>> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<User>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				var expression1 = expression;
				expression = expression1.And(x => x.UserName.Contains(sortBuider.Keywords));
			}
			return await FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}