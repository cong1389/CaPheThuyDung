using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Domain.Interfaces.Services;

namespace App.Service.Ads
{
    public interface IPageBannerService : IBaseService<PageBanner>
    {
        PageBanner GetById(int id, bool isCache = true);

        IEnumerable<PageBanner> PagedList(SortingPagingBuilder sortBuider, Paging page);
    }
}