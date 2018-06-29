using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.GenericControl
{
    public interface IGenericControlService : IBaseService<Domain.Entities.GenericControl.GenericControl>
    {
        Domain.Entities.GenericControl.GenericControl GetById(int id, bool isCache = true);

        IEnumerable<Domain.Entities.GenericControl.GenericControl> GetByMenuId(int menuId, bool isCache = true);

        IEnumerable<Domain.Entities.GenericControl.GenericControl> PagedList(SortingPagingBuilder sortBuider, Paging page);
    }
}