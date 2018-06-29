using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Brandes;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Brandes
{
    public interface IBrandRepository : IRepositoryBase<Brand>
	{
		Brand GetById(int id);

		IEnumerable<Brand> PagedList(Paging page);

		IEnumerable<Brand> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}