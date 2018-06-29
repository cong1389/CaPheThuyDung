using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.GenericControl;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.GenericControl
{
    public class GenericControlService : BaseService<Domain.Entities.GenericControl.GenericControl>, IGenericControlService
    {
        private const string CacheGenericcontrolKey = "db.GenericControl.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IGenericControlRepository _attributeRepository;

        public GenericControlService(IUnitOfWork unitOfWork, IGenericControlRepository attributeRepository
            , ICacheManager cacheManager) : base(unitOfWork, attributeRepository)
        {
            _attributeRepository = attributeRepository;
            _cacheManager = cacheManager;
        }

        public Domain.Entities.GenericControl.GenericControl GetById(int id, bool isCache = true)
        {
            Domain.Entities.GenericControl.GenericControl genericControl;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheGenericcontrolKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                genericControl = _cacheManager.Get<Domain.Entities.GenericControl.GenericControl>(key);
                if (genericControl == null)
                {
                    genericControl = _attributeRepository.GetById(id);
                    _cacheManager.Put(key, genericControl);
                }
            }
            else
            {
                genericControl = _attributeRepository.GetById(id);
            }
            
            return genericControl;
        }

        public IEnumerable<Domain.Entities.GenericControl.GenericControl> GetByMenuId(int menuId, bool isCache = true)
        {
            IEnumerable<Domain.Entities.GenericControl.GenericControl> genericControls;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheGenericcontrolKey, "GetByMenuId");
                sbKey.AppendFormat("-{0}", menuId);

                var key = sbKey.ToString();
                genericControls = _cacheManager.GetCollection<Domain.Entities.GenericControl.GenericControl>(key);
                if (genericControls == null)
                {
                    genericControls = _attributeRepository.FindBy(x => x.MenuId == menuId);
                    _cacheManager.Put(key, genericControls);
                }
            }
            else
            {
                genericControls = _attributeRepository.FindBy(x => x.MenuId == menuId);
            }

            return genericControls;
        }

        public IEnumerable<Domain.Entities.GenericControl.GenericControl> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _attributeRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}