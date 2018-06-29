using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Extensions;
using App.Core.Utilities;
using App.Domain.Entities.Payments;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.PaymentMethodes;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.PaymentMethodes
{
    public class PaymentMethodService : BaseService<PaymentMethod>, IPaymentMethodService
    {
        private const string CachePaymentMethodKey = "db.PaymentMethod.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IUnitOfWork unitOfWork, IPaymentMethodRepository paymentMethodRepository
            , ICacheManager cacheManager) : base(unitOfWork, paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _cacheManager = cacheManager;
        }

        public IEnumerable<PaymentMethod> GetAll(bool isCache = true)
        {
            IEnumerable<PaymentMethod> iePaymentMethod;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CachePaymentMethodKey, "GetAll");

                var key = sbKey.ToString();
                iePaymentMethod = _cacheManager.GetCollection<PaymentMethod>(key);
                if (iePaymentMethod == null)
                {
                    iePaymentMethod = _paymentMethodRepository.GetAll();
                    _cacheManager.Put(key, iePaymentMethod);
                }
            }
            else
            {
                iePaymentMethod = _paymentMethodRepository.GetAll();

            }

            return iePaymentMethod;
        }

        public PaymentMethod GetById(int id)
        {
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CachePaymentMethodKey, "GetBySeoUrls");
            sbKey.AppendFormat("-{0}", id);

            var key = sbKey.ToString();
            var paymentMethod = _cacheManager.Get<PaymentMethod>(key);

            if (paymentMethod != null)
            {
                return paymentMethod;
            }

            paymentMethod = _paymentMethodRepository.GetById(id);
            _cacheManager.Put(key, paymentMethod);

            return paymentMethod;
        }

        public PaymentMethod GetBySystemName(string systemName)
        {
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CachePaymentMethodKey, "GetBySystemName");

            if (systemName.HasValue())
            {
                sbKey.AppendFormat("-{0}", systemName);
            }

            var key = sbKey.ToString();
            var paymentMethod = _cacheManager.Get<PaymentMethod>(key);

            if (paymentMethod != null)
            {
                return paymentMethod;
            }

            paymentMethod = _paymentMethodRepository.Get(x => x.PaymentMethodSystemName == systemName);

            _cacheManager.Put(key, paymentMethod);

            return paymentMethod;
        }

        public IEnumerable<PaymentMethod> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _paymentMethodRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}