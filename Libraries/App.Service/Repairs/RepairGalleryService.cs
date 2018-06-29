using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Repairs;
using App.Infra.Data.UOW.Interfaces;
using App.Service.Repair;

namespace App.Service.Repairs
{
    public class RepairGalleryService : BaseService<RepairGallery>, IRepairGalleryService
	{
	    public RepairGalleryService(IUnitOfWork unitOfWork, IRepairGalleryRepository galleryRepository) : base(unitOfWork, galleryRepository)
		{
		}
	}
}