using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Location;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Locations;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Locations
{
    public class DistrictService : BaseService<District>, IDistrictService
	{
		private readonly IDistrictRepository _districtRepository;

	    public DistrictService(IUnitOfWork unitOfWork, IDistrictRepository districtRepository) : base(unitOfWork, districtRepository)
		{
		    _districtRepository = districtRepository;
		}

		public District GetById(int id)
		{
			return _districtRepository.GetById(id);
		}

		public IEnumerable<District> GetByProvinceId(int provinceId)
		{
			var districts = _districtRepository.FindBy(x => x.ProvinceId == provinceId);
			return districts;
		}

		public IEnumerable<District> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _districtRepository.PagedSearchList(sortbuBuilder, page);
		}
	}
}