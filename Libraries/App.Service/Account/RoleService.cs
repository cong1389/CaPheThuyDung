using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Utilities;
using App.Domain.Entities.Account;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Account;

namespace App.Service.Account
{
    public class RoleService : BaseAsyncService<Role>, IRoleService
	{
	    private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository roleRepository) : base(roleRepository)
		{
            _roleRepository = roleRepository;
		}

		public Task<IEnumerable<Role>> PagedList(SortingPagingBuilder sortBuider, Paging page)
		{
			return _roleRepository.PagedSearchList(sortBuider, page);
		}
	}
}