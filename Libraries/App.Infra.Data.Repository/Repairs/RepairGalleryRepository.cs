using System.Linq;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Repairs
{
    public class RepairGalleryRepository : RepositoryBase<RepairGallery>, IRepairGalleryRepository
	{
		public RepairGalleryRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

        protected override IOrderedQueryable<RepairGallery> GetDefaultOrder(IQueryable<RepairGallery> query)
        {
            var repairGalleries =
                from p in query
                orderby p.Id
                select p;
            return repairGalleries;
        }
    }
}