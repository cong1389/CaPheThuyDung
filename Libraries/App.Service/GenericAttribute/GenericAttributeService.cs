using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.GenericAttribute;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.GenericAttribute
{
    public class GenericAttributeService : BaseService<Domain.Entities.Data.GenericAttribute>, IGenericAttributeService
    {
        private const string CacheGenericattributeKey = "db.GenericAttribute.{0}";
        private readonly ICacheManager _cacheManager;
        private readonly IGenericAttributeRepository _genericAttributeRepository;

        public GenericAttributeService(IUnitOfWork unitOfWork, IGenericAttributeRepository genericAttributeRepository,
            ICacheManager cacheManager) : base(unitOfWork, genericAttributeRepository)
        {
            _genericAttributeRepository = genericAttributeRepository;
            _cacheManager = cacheManager;
        }

        public void CreateGenericAttribute(Domain.Entities.Data.GenericAttribute genericAttribute)
        {
            _genericAttributeRepository.Add(genericAttribute);
        }

        public Domain.Entities.Data.GenericAttribute GetById(int id, bool isCache = true)
        {
            Domain.Entities.Data.GenericAttribute genericAttribute;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheGenericattributeKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                genericAttribute = _cacheManager.Get<Domain.Entities.Data.GenericAttribute>(key);
                if (genericAttribute == null)
                {
                    genericAttribute = _genericAttributeRepository.GetAttributeById(id);
                    _cacheManager.Put(key, genericAttribute);
                }
            }
            else
            {
                genericAttribute= _genericAttributeRepository.GetAttributeById(id);
            }

            return genericAttribute;
        }

        public Domain.Entities.Data.GenericAttribute GetByKey(int entityId, string keyGroup, string key, bool isCache = true)
        {
            Domain.Entities.Data.GenericAttribute attr;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheGenericattributeKey, "GetByKey");
                sbKey.Append(entityId);

                if (!string.IsNullOrWhiteSpace(keyGroup))
                {
                    sbKey.Append(keyGroup);
                }

                if (!string.IsNullOrWhiteSpace(key))
                {
                    sbKey.Append(key);
                }

                var keyCachare = sbKey.ToString();
                attr = _cacheManager.Get<Domain.Entities.Data.GenericAttribute>(keyCachare);

                if (attr != null)
                {
                    return attr;
                }

                attr = _genericAttributeRepository
                    .Get(x =>
                        x.EntityId.Equals(entityId)
                        && x.KeyGroup.Equals(keyGroup)
                        && x.Key.Equals(key));

                _cacheManager.Put(keyCachare, attr);
            }
            else
            {
                attr = _genericAttributeRepository
               .Get(x =>
               x.EntityId.Equals(entityId)
                && x.KeyGroup.Equals(keyGroup)
                && x.Key.Equals(key));
            }

            return attr;
        }

        public IEnumerable<Domain.Entities.Data.GenericAttribute> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _genericAttributeRepository.PagedSearchList(sortbuBuilder, page);
        }

        public void SaveGenericAttribute(int entityId, string keyGroup, string key, string value, int storeId = 0)
        {
            var objAttribute = new Domain.Entities.Data.GenericAttribute
            {
                EntityId = entityId,
                KeyGroup = keyGroup,
                Key = key,
                Value = value,
                StoreId = storeId
            };

            var attribute = GetByKey(entityId, keyGroup, key);

            if (attribute == null)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Create(objAttribute);
                }
            }
            else
            {
                //Không có value thì delete
                if (string.IsNullOrWhiteSpace(value))
                {
                    Delete(attribute);
                }
                else
                {
                    attribute.Value = value;
                    Update(attribute);
                }
            }
        }
    }
}