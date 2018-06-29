using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Common;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Addresses;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Addresses
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        private const string CacheKey = "db.Address.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IAddressRepository _addressRepository;

        public AddressService(IUnitOfWork unitOfWork, IAddressRepository addressRepository, ICacheManager cacheManager) : base(unitOfWork, addressRepository)
        {
            _addressRepository = addressRepository;
            _cacheManager = cacheManager;
        }

        public Address GetById(int id, bool isCache = true)
        {
            Address address;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                address = _cacheManager.Get<Address>(key);
                if (address == null)
                {
                    address = _addressRepository.GetById(id);
                    _cacheManager.Put(key, address);
                }

            }
            else
            {
                address = _addressRepository.GetById(id);
            }            

            return address;
        }

        public IEnumerable<Address> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _addressRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}