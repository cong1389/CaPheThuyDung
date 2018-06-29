using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GenericControl;
using App.Domain.Interfaces.Services;

namespace App.Service.GenericControl
{
	public interface IGenericControlValueService : IBaseService<GenericControlValue>
	{
		GenericControlValue GetById(int id, bool isCache = true);

        IEnumerable<GenericControlValue> GetByEntityId(int entityId);

        IEnumerable<GenericControlValue> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}