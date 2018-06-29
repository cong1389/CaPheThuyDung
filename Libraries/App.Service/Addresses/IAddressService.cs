using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Common;
using App.Domain.Interfaces.Services;

namespace App.Service.Addresses
{
    public interface IAddressService : IBaseService<Address>
	{
		Address GetById(int id, bool isCache = true);

		IEnumerable<Address> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}