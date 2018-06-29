using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Brandes;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Brandes;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Brandes
{
    public class BrandService : BaseService<Brand>, IBrandService
	{
		private readonly IBrandRepository _brandRepository;

	    public BrandService(IUnitOfWork unitOfWork, IBrandRepository brandRepository) : base(unitOfWork, brandRepository)
		{
		    _brandRepository = brandRepository;
		}

		public Brand GetById(int id)
		{
			return _brandRepository.GetById(id);
		}

		public IEnumerable<Brand> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _brandRepository.PagedSearchList(sortbuBuilder, page);
		}
	}
}