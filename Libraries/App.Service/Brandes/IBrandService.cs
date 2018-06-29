using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Brandes;
using App.Domain.Interfaces.Services;

namespace App.Service.Brandes
{
    public interface IBrandService : IBaseService<Brand>
	{
		Brand GetById(int id);

		IEnumerable<Brand> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}