using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Ads
{
	public interface IPageBannerRepository : IRepositoryBase<PageBanner>
	{
		PageBanner GetById(int id);

		IEnumerable<PageBanner> PagedList(Paging page);

		IEnumerable<PageBanner> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}