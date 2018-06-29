using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.News
{
    public interface INewsService : IBaseService<Domain.Entities.Data.News>
    {
        Domain.Entities.Data.News GetById(int id, bool isCache = true);

        IEnumerable<Domain.Entities.Data.News> GetBySeoUrl(string seoUrl, bool isCache = true);

        IEnumerable<Domain.Entities.Data.News> PagedList(SortingPagingBuilder sortBuider, Paging page);

        IEnumerable<Domain.Entities.Data.News> PagedListByMenu(SortingPagingBuilder sortBuider, Paging page);

        IEnumerable<Domain.Entities.Data.News> GetByOption(string virtualCategoryId = null
            , bool? isDisplayHomePage = null
           , bool? isVideo = null
           , int status = 1
           , bool isCache = true);

        IEnumerable<Domain.Entities.Data.News> GetEnableOrDisables(bool enable = true, bool isCache = true);

        Domain.Entities.Data.News GetEnableOrDisable(bool enable = true, bool isCache = true);
    }
}