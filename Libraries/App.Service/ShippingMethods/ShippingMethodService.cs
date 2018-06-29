using System.Collections.Generic;
using App.Core.Plugins.Providers;
using App.Core.Utilities;
using App.Domain.Shippings;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.ShippingMethods;
using App.Infra.Data.UOW.Interfaces;
using App.Service.ShippingMethods;

namespace App.Service.ShippingMethodes
{
    public class ShippingMethodService : BaseService<ShippingMethod>, IShippingMethodService
	{
		private readonly IShippingMethodRepository _shippingMethodRepository;

		private readonly IProviderManager _providerManager;

		public ShippingMethodService(IUnitOfWork unitOfWork, IShippingMethodRepository shippingMethodRepository, IProviderManager providerManager) : base(unitOfWork, shippingMethodRepository)
		{
			_shippingMethodRepository = shippingMethodRepository;
			_providerManager = providerManager;
		}

		public ShippingMethod GetById(int id)
		{
			_providerManager.GetAllProviders<IShippingRateComputationMethod>();
			return _shippingMethodRepository.GetById(id);
		}

		public IEnumerable<ShippingMethod> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _shippingMethodRepository.PagedSearchList(sortbuBuilder, page);
		}
	}
}