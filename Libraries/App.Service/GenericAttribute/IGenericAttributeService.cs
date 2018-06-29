using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.GenericAttribute
{
    public interface IGenericAttributeService : IBaseService<Domain.Entities.Data.GenericAttribute>
    {
        void CreateGenericAttribute(Domain.Entities.Data.GenericAttribute attribute);

        Domain.Entities.Data.GenericAttribute GetById(int id, bool isCache = true);

        Domain.Entities.Data.GenericAttribute GetByKey(int entityId, string keyGroup, string key, bool isCache = true);

        IEnumerable<Domain.Entities.Data.GenericAttribute> PagedList(SortingPagingBuilder sortBuider, Paging page);

        void SaveGenericAttribute(int entityId, string keyGroup, string key, string value, int storeId = 0);
    }
}