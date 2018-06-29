using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Domain.Interfaces.Services;

namespace App.Service.Account
{
    public interface IUserService : IBaseAsyncService<User>
	{
		Task<IEnumerable<User>> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}