using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Common;
using App.Domain.Entities.GlobalSetting;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.System;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.SystemApp
{
    public class SystemSettingService : BaseService<SystemSetting>, ISystemSettingService
    {
        private const string CacheKey = "db.SystemSetting.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly ISystemSettingRepository _systemSettingRepository;

        public SystemSettingService(IUnitOfWork unitOfWork, ISystemSettingRepository systemSettingRepository
            , ICacheManager cacheManager) : base(unitOfWork, systemSettingRepository)
        {
            _systemSettingRepository = systemSettingRepository;
            _cacheManager = cacheManager;
        }

        public SystemSetting GetById(int id, bool isCache = true)
        {
            if (!isCache)
            {
                return _systemSettingRepository.GetById(id); 
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetById");
            sbKey.Append(id);

            var key = sbKey.ToString();

            var systemSetting = _cacheManager.Get<SystemSetting>(key);
            if (systemSetting == null)
            {
                systemSetting = _systemSettingRepository.GetById(id);
                _cacheManager.Put(key, systemSetting);
            }

            return systemSetting; 
        }

        public SystemSetting GetEnableOrDisable(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _systemSettingRepository.Get(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable));
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetEnableOrDisable");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var systemSetting = _cacheManager.Get<SystemSetting>(key);
            if (systemSetting == null)
            {
                systemSetting = _systemSettingRepository.Get(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable));
                _cacheManager.Put(key, systemSetting);
            }

            return systemSetting;
        }

        public IEnumerable<SystemSetting> GetEnableOrDisables(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _systemSettingRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetEnableOrDisables");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var systemSettings = _cacheManager.GetCollection<SystemSetting>(key);
            if (systemSettings == null)
            {
                systemSettings = _systemSettingRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
                _cacheManager.Put(key, systemSettings);
            }

            return systemSettings;
        }
        
        public IEnumerable<SystemSetting> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _systemSettingRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}
