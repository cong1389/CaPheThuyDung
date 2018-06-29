using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Attribute
{
    public interface IAttributeRepository : IRepositoryBase<Domain.Entities.Attribute.Attribute>
	{
		Domain.Entities.Attribute.Attribute GetById(int id);

		IEnumerable<Domain.Entities.Attribute.Attribute> PagedList(Paging page);

		IEnumerable<Domain.Entities.Attribute.Attribute> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}