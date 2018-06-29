using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Ads
{
    public interface IBannerRepository : IRepositoryBase<Banner>
	{
		Banner GetById(int id);

		IEnumerable<Banner> PagedList(Paging page);

		IEnumerable<Banner> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}