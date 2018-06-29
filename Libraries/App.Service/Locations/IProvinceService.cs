using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.Domain.Interfaces.Services;

namespace App.Service.Locations
{
	public interface IProvinceService : IBaseService<Province>
	{
		Province GetById(int id);

		IEnumerable<Province> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}