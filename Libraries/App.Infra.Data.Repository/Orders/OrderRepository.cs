using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Orders;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Orderes
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Order GetById(int id)
        {
            var order = FindBy(x => x.Id == id).FirstOrDefault();
            return order;
        }

        public IEnumerable<Order> GetByCustomerId(int customerId)
        {
            var order = FindBy(x => x.CustomerId == customerId);
            return order;
        }

        protected override IOrderedQueryable<Order> GetDefaultOrder(IQueryable<Order> query)
        {
            var order =
                from p in query
                orderby p.Id
                select p;
            return order;
        }

        public IEnumerable<Order> PagedList(Paging page)
        {
            return GetAllPagedList(page).ToList();
        }

        public IEnumerable<Order> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
        {
            var expression = PredicateBuilder.True<Order>();
            if (!string.IsNullOrEmpty(sortBuider.Keywords))
            {
                expression = expression.And(x => x.Id.ToString().ToLower().Contains(sortBuider.Keywords.ToLower()));
            }
            return FindAndSort(expression, sortBuider.Sorts, page);
        }
    }
}