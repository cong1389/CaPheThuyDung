using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;

namespace App.Service.Post
{
    public interface IPostService : IBaseService<Domain.Entities.Data.Post>
    {
        Domain.Entities.Data.Post GetById(int id, bool isCache = true);

        IEnumerable<Domain.Entities.Data.Post> GetListSeoUrl(string seoUrl, bool isCache = true);

        Domain.Entities.Data.Post GetBySeoUrl(string seoUrl, bool @readonly = false);

        IEnumerable<Domain.Entities.Data.Post> PagedList(SortingPagingBuilder sortBuider, Paging page);

        IEnumerable<Domain.Entities.Data.Post> PagedListByMenu(SortingPagingBuilder sortBuider, Paging page);

        IEnumerable<Domain.Entities.Data.Post> GetBySort(Expression<Func<Domain.Entities.Data.Post, bool>> expression, SortBuilder sortBuilder, Paging paging);

        IEnumerable<Domain.Entities.Data.Post> GetByOption(string virtualCategoryId = null,
            bool? isDisplayHomePage = null, bool? isDiscount = null, int status = 1, bool isCache = true);

    }
}