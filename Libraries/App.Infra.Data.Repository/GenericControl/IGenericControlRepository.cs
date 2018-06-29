using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.GenericControl
{
    public interface IGenericControlRepository : IRepositoryBase<Domain.Entities.GenericControl.GenericControl>
	{
		Domain.Entities.GenericControl.GenericControl GetById(int id);

		IEnumerable<Domain.Entities.GenericControl.GenericControl> PagedList(Paging page);

		IEnumerable<Domain.Entities.GenericControl.GenericControl> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}