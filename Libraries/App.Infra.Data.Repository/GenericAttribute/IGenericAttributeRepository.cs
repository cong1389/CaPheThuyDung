using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.GenericAttribute
{
	public interface IGenericAttributeRepository : IRepositoryBase<Domain.Entities.Data.GenericAttribute>
	{
		Domain.Entities.Data.GenericAttribute GetAttributeById(int id);

		IEnumerable<Domain.Entities.Data.GenericAttribute> PagedList(Paging page);

		IEnumerable<Domain.Entities.Data.GenericAttribute> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}