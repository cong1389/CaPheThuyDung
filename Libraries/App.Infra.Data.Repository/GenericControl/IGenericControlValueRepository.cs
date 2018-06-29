using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GenericControl;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.GenericControl
{
	public interface IGenericControlValueRepository : IRepositoryBase<GenericControlValue>
	{
		GenericControlValue GetById(int id);

		IEnumerable<GenericControlValue> PagedList(Paging page);

		IEnumerable<GenericControlValue> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}