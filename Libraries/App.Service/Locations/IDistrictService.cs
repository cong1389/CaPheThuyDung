using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.Domain.Interfaces.Services;

namespace App.Service.Locations
{
	public interface IDistrictService : IBaseService<District>
	{
		District GetById(int id);

		IEnumerable<District> GetByProvinceId(int provinceId);

		IEnumerable<District> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}