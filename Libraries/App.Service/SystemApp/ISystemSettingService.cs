using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Interfaces.Services;

namespace App.Service.SystemApp
{
    public interface ISystemSettingService : IBaseService<SystemSetting>
    {
        SystemSetting GetById(int id, bool isCache = true);
        SystemSetting GetEnableOrDisable(bool enable = true, bool isCache = true);
        IEnumerable<SystemSetting> GetEnableOrDisables(bool enable = true, bool isCache = true);
        IEnumerable<SystemSetting> PagedList(SortingPagingBuilder sortBuider, Paging page);
    }
}
