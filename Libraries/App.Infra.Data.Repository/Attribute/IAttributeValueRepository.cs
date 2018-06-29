using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Attribute;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Attribute
{
    public interface IAttributeValueRepository : IRepositoryBase<AttributeValue>
	{
		AttributeValue GetById(int id);

		IEnumerable<AttributeValue> PagedList(Paging page);

		IEnumerable<AttributeValue> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}