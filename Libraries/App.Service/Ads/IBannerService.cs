using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Domain.Interfaces.Services;

namespace App.Service.Ads
{
    public interface IBannerService : IBaseService<Banner>
    {
        Banner GetById(int id, bool isCache = true);

        Banner GetBanner(int? menuId = null, int status = 1, List<int> position = null, bool isCache = true);

        IEnumerable<Banner> GetBanners(int? menuId = null, int status = 1, List<int> position = null,
            bool isCache = true);

        IEnumerable<Banner> PagedList(SortingPagingBuilder sortBuider, Paging page);
    }
}