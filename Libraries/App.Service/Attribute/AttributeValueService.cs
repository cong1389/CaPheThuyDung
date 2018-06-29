using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Attribute;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Attribute;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Attribute
{
    public class AttributeValueService : BaseService<AttributeValue>, IAttributeValueService
    {
        private const string CacheAttributevalueKey = "db.AttributeValue.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IAttributeValueRepository _attributeValueRepository;

        public AttributeValueService(IUnitOfWork unitOfWork, IAttributeValueRepository attributeValueRepository
            , ICacheManager cacheManager) : base(unitOfWork, attributeValueRepository)
        {
            _attributeValueRepository = attributeValueRepository;
            _cacheManager = cacheManager;
        }

        public AttributeValue GetById(int id, bool isCache = true)
        {
            AttributeValue attributeValue;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheAttributevalueKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                attributeValue = _cacheManager.Get<AttributeValue>(key);
                if (attributeValue == null)
                {
                    attributeValue = _attributeValueRepository.GetById(id);
                    _cacheManager.Put(key, attributeValue);
                }
            }
            else
            {
                attributeValue = _attributeValueRepository.GetById(id);
            }

            return attributeValue;
        }

        public IEnumerable<AttributeValue> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _attributeValueRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}