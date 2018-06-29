using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.Repairs
{
    public interface IRepairService : IBaseService<Domain.Entities.Data.Repair>
    {
        IEnumerable<Domain.Entities.Data.Repair> PagedList(SortingPagingBuilder sortBuider, Paging page);
    }
}