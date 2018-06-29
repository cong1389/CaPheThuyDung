using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.Attribute
{
	public interface IAttributeService : IBaseService<Domain.Entities.Attribute.Attribute>
	{
		Domain.Entities.Attribute.Attribute GetById(int id, bool isCache = true);

		IEnumerable<Domain.Entities.Attribute.Attribute> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}