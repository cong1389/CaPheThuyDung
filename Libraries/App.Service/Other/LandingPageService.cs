using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Other;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Other;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Other
{
    public class LandingPageService : BaseService<LandingPage>, ILandingPageService
	{
		private readonly ILandingPageRepository _landingPageRepository;

	    public LandingPageService(IUnitOfWork unitOfWork, ILandingPageRepository landingPageRepository) : base(unitOfWork, landingPageRepository)
		{
		    _landingPageRepository = landingPageRepository;
		}

		public IEnumerable<LandingPage> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _landingPageRepository.PagedSearchList(sortbuBuilder, page);
		}
	}
}