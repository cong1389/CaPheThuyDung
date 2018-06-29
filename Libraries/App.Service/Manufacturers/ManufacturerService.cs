using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Manufacturers;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Manufacturers
{
	public class ManufacturerService : BaseService<Manufacturer>, IManufacturerService
	{
		private readonly IManufacturerRepository _manufacturerRepository;

	    public ManufacturerService(IUnitOfWork unitOfWork, IManufacturerRepository manufacturerRepository) : base(unitOfWork, manufacturerRepository)
		{
		    _manufacturerRepository = manufacturerRepository;
		}

		public IEnumerable<Manufacturer> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _manufacturerRepository.PagedSearchList(sortbuBuilder, page);
		}

		public IEnumerable<Manufacturer> PagedListByMenu(SortingPagingBuilder sortBuider, Paging page)
		{
			return _manufacturerRepository.PagedSearchListByMenu(sortBuider, page);
		}
	}
}