using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Locations
{
	public interface IProvinceRepository : IRepositoryBase<Province>
	{
		Province GetById(int id);

		IEnumerable<Province> PagedList(Paging page);

		IEnumerable<Province> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}