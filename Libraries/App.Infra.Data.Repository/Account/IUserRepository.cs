using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Account
{
    public interface IUserRepository : IRepositoryBaseAsync<User>
	{
		User FindByEmail(string email);

		Task<User> FindByEmailAsync(string email);

		Task<User> FindByEmailAsync(CancellationToken cancellationToken, string email);

		User FindByUserName(string username);

		Task<User> FindByUserNameAsync(string username);

		Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);

		Task<IEnumerable<User>> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}