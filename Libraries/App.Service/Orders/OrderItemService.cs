using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Orders;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Orders;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Orders
{
    public class OrderItemService : BaseService<OrderItem>, IOrderItemService
	{
		private readonly IOrderItemRepository _orderItemRepository;

	    public OrderItemService(IUnitOfWork unitOfWork, IOrderItemRepository orderItemRepository) : base(unitOfWork, orderItemRepository)
		{
		    _orderItemRepository = orderItemRepository;
		}

		public OrderItem GetById(int id)
		{
			return _orderItemRepository.GetById(id);
		}

        public OrderItem GetByPostId(int postId)
        {
            return _orderItemRepository.Get(x => x.PostId == postId);
        }

        public IEnumerable<OrderItem> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _orderItemRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}