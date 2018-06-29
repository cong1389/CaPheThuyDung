using System.Linq;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Repairs
{
    public class RepairItemRepository : RepositoryBase<RepairItem>, IRepairItemRepository
    {
        public RepairItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        protected override IOrderedQueryable<RepairItem> GetDefaultOrder(IQueryable<RepairItem> query)
        {
            var orderItems =
                from p in query
                orderby p.Id
                select p;
            return orderItems;
        }
    }
}