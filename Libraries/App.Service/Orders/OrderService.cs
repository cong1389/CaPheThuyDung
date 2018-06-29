using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Orders;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Orderes;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Orders
{
    public class OrderService : BaseService<Order>, IOrderService
	{
        private const string CacheOrderKey = "db.Order.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IOrderRepository _orderRepository;

	    public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository, ICacheManager cacheManager) : base(unitOfWork, orderRepository)
		{
		    _orderRepository = orderRepository;
            _cacheManager = cacheManager;

        }

		public Order GetById(int id, bool isCache = true)
        {
            Order order;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheOrderKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                order = _cacheManager.Get<Order>(key);
                if (order == null)
                {
                    order = _orderRepository.GetById(id);
                    _cacheManager.Put(key, order);
                }
            }
            else
            {
                order = _orderRepository.GetById(id);
            }

            return order;
		}

        public IEnumerable<Order> GetByCustomerId(int customerId, bool isCache = true)
        {
            IEnumerable<Order> ieOrder;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheOrderKey, "GetByCustomerId");
                sbKey.AppendFormat("-{0}", customerId);

                var key = sbKey.ToString();
                ieOrder = _cacheManager.GetCollection<Order>(key);
                if (ieOrder == null)
                {
                    ieOrder = _orderRepository.GetByCustomerId(customerId);
                    _cacheManager.Put(key, ieOrder);
                }
            }
            else
            {
                ieOrder = _orderRepository.GetByCustomerId(customerId);
            }     

            return ieOrder;
        }

        public IEnumerable<Order> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _orderRepository.PagedSearchList(sortbuBuilder, page);
		}
	}
}