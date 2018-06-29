using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Account
{
    public interface IRoleRepository : IRepositoryBaseAsync<Role>
	{
		Role FindByName(string roleName);

		Task<Role> FindByNameAsync(string roleName);

		Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName);

		Task<IEnumerable<Role>> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}