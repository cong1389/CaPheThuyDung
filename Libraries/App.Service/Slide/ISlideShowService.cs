using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Slide;
using App.Domain.Interfaces.Services;

namespace App.Service.Slide
{
    public interface ISlideShowService : IBaseService<SlideShow>
    {
        IEnumerable<SlideShow> PagedList(SortingPagingBuilder sortBuider, Paging page);
        IEnumerable<SlideShow> GetEnableOrDisables(bool enable = true, bool isCache = true);
    }
}