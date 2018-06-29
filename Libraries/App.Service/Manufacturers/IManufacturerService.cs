using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Domain.Interfaces.Services;

namespace App.Service.Manufacturers
{
	public interface IManufacturerService : IBaseService<Manufacturer>
	{
		IEnumerable<Manufacturer> PagedList(SortingPagingBuilder sortBuider, Paging page);

		IEnumerable<Manufacturer> PagedListByMenu(SortingPagingBuilder sortBuider, Paging page);
	}
}