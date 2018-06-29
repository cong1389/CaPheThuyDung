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
    public class RoleRepository : RepositoryBaseAsync<Role>, IRoleRepository
	{
		public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Role FindByName(string roleName)
		{
			var role = Get(x => x.Name.Equals(roleName));
			return role;
		}

		public Task<Role> FindByNameAsync(string roleName)
		{
			var async = GetAsync(x => x.Name.Equals(roleName));
			return async;
		}

		public Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName)
		{
			var async = GetAsync(cancellationToken, x => x.Name.Equals(roleName));
			return async;
		}

		protected override IOrderedQueryable<Role> GetDefaultOrder(IQueryable<Role> query)
		{
			var roles = 
				from p in query
				orderby p.Name
				select p;
			return roles;
		}

		public async Task<IEnumerable<Role>> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Role>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				var expression1 = expression;
				expression = expression1.And(x => x.Name.Contains(sortBuider.Keywords));
			}
			return await FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}