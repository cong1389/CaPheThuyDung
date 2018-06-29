using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Common;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.ContactInformation;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.ContactInformation
{
    public class ContactInfoService : BaseService<Domain.Entities.GlobalSetting.ContactInformation>, IContactInfoService
    {
        private const string CacheKey = "db.ContactInfo.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IContactInfoRepository _contactInfoRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ContactInfoService(IUnitOfWork unitOfWork, IContactInfoRepository contactInfoRepository
            , ICacheManager cacheManager) : base(unitOfWork, contactInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _contactInfoRepository = contactInfoRepository;
            _cacheManager = cacheManager;
        }

        public Domain.Entities.GlobalSetting.ContactInformation GetById(int id, bool isCache = true)
        {
            Domain.Entities.GlobalSetting.ContactInformation contactInformation;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                contactInformation = _cacheManager.Get<Domain.Entities.GlobalSetting.ContactInformation>(key);
                if (contactInformation == null)
                {
                    contactInformation = _contactInfoRepository.GetById(id);
                    _cacheManager.Put(key, contactInformation);
                }
            }
            else
            {
                contactInformation = _contactInfoRepository.GetById(id);
            }


            return contactInformation;
        }

        public IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _contactInfoRepository.PagedSearchList(sortbuBuilder, page);
        }

        public int Save()
        {
            return _unitOfWork.Commit();
        }

        public Domain.Entities.GlobalSetting.ContactInformation GetTypeAddress(int typeAddress, bool isCache = true)
        {
            Domain.Entities.GlobalSetting.ContactInformation contactInformation;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetTypeAddress");
                sbKey.Append(typeAddress);

                var key = sbKey.ToString();
                contactInformation = _cacheManager.Get<Domain.Entities.GlobalSetting.ContactInformation>(key);
                if (contactInformation == null)
                {
                    contactInformation = _contactInfoRepository.Get(x => x.Status == 1 && x.Type == typeAddress, true);
                    _cacheManager.Put(key, contactInformation);
                }
            }
            else
            {
                contactInformation = _contactInfoRepository.GetById(typeAddress);
            }


            return contactInformation;
        }

        public IEnumerable<Domain.Entities.GlobalSetting.ContactInformation> GetEnableOrDisables(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _contactInfoRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetEnableOrDisables");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var slideShows = _cacheManager.GetCollection<Domain.Entities.GlobalSetting.ContactInformation>(key);
            if (slideShows == null)
            {
                slideShows = _contactInfoRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
                _cacheManager.Put(key, slideShows);
            }

            return slideShows;
        }
    }
}