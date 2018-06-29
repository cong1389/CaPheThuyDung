using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.GenericControl;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.GenericControl;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.GenericControl
{
    public class GenericControlValueItemService : BaseService<GenericControlValueItem>, IGenericControlValueItemService
    {
        private const string CacheGenericcontrolvalueitemKey = "db.GenericControlValueItem.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IGenericControlValueItemRepository _genericControlValueItemRepository;

        public GenericControlValueItemService(IUnitOfWork unitOfWork, IGenericControlValueItemRepository genericControlValueItemRepository
            , ICacheManager cacheManager) : base(unitOfWork, genericControlValueItemRepository)
        {
            _genericControlValueItemRepository = genericControlValueItemRepository;
            _cacheManager = cacheManager;

        }

        public GenericControlValueItem GetById(int id, bool isCache = true)
        {
            GenericControlValueItem genericControlValueItem;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheGenericcontrolvalueitemKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                genericControlValueItem = _cacheManager.Get<GenericControlValueItem>(key);
                if (genericControlValueItem == null)
                {
                    genericControlValueItem = _genericControlValueItemRepository.GetById(id);
                    _cacheManager.Put(key, genericControlValueItem);
                }
            }
            else
            {
                genericControlValueItem = _genericControlValueItemRepository.GetById(id);
            }          

            return genericControlValueItem;
        }

        public IEnumerable<GenericControlValueItem> GetByOption(int? genericControlValueId = null
            , int? entityId = null
            , int status = 1
            , bool isCache = true)
        {
            IEnumerable<GenericControlValueItem> genericControlValueItem;
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheGenericcontrolvalueitemKey, "GetByOption");

            var expression = PredicateBuilder.True<GenericControlValueItem>();
            sbKey.AppendFormat("-{0}", status);
            expression = expression.And(x => x.Status == status);

            if (genericControlValueId != null)
            {
                sbKey.AppendFormat("-{0}", genericControlValueId);
                expression = expression.And(x => x.GenericControlValueId == genericControlValueId);
            }

            if (entityId != null)
            {
                sbKey.AppendFormat("-{0}", entityId);
                expression = expression.And(x => x.EntityId == entityId);
            }

            if (isCache)
            {
                var key = sbKey.ToString();
                genericControlValueItem = _cacheManager.GetCollection<GenericControlValueItem>(key);
                if (genericControlValueItem.Any())
                {
                    genericControlValueItem = FindBy(expression);
                    _cacheManager.Put(key, genericControlValueItem);
                }
            }
            else
            {
                genericControlValueItem = FindBy(expression);
            }


            return genericControlValueItem;
        }
    }
}