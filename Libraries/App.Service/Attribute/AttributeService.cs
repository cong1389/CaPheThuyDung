using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Attribute;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Attribute
{
    public class AttributeService : BaseService<Domain.Entities.Attribute.Attribute>, IAttributeService
    {
        private const string CacheAttributeKey = "db.Attribute.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IAttributeRepository _attributeRepository;

        public AttributeService(IUnitOfWork unitOfWork, IAttributeRepository attributeRepository
            , ICacheManager cacheManager) : base(unitOfWork, attributeRepository)
        {
            _attributeRepository = attributeRepository;
            _cacheManager = cacheManager;
        }

        public Domain.Entities.Attribute.Attribute GetById(int id, bool isCache = true)
        {
            Domain.Entities.Attribute.Attribute attribute;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheAttributeKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                attribute = _cacheManager.Get<Domain.Entities.Attribute.Attribute>(key);
                if (attribute == null)
                {
                    attribute = _attributeRepository.GetById(id);
                    _cacheManager.Put(key, attribute);
                }
            }
            else
            {
                attribute = _attributeRepository.GetById(id);
            }

            return attribute;
        }

        public IEnumerable<Domain.Entities.Attribute.Attribute> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _attributeRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}