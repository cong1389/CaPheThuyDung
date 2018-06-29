using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Domain.Interfaces.Services;

namespace App.Service.Account
{
    public interface IRoleService : IBaseAsyncService<Role>
	{
		Task<IEnumerable<Role>> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}