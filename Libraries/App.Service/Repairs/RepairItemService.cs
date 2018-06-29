using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Repairs;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Repairs
{
    public class RepairItemService : BaseService<RepairItem>, IRepairItemService
	{
	    public RepairItemService(IUnitOfWork unitOfWork, IRepairItemRepository orderItemRepository) : base(unitOfWork, orderItemRepository)
		{
		}
	}
}