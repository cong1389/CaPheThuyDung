using System.Collections.Generic;
using System.Text;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Common;
using App.Domain.Entities.Slide;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Slide;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Slide
{
    public class SlideShowService : BaseService<SlideShow>, ISlideShowService
    {
        private const string CacheKey = "db.SlideShow.{0}";
        private readonly ICacheManager _cacheManager;
        private readonly ISlideShowRepository _slideShowRepository;

        public SlideShowService(IUnitOfWork unitOfWork, ISlideShowRepository slideShowRepository, ICacheManager cacheManager) : base(unitOfWork, slideShowRepository)
        {
            _slideShowRepository = slideShowRepository;
            _cacheManager = cacheManager;
        }

        public IEnumerable<SlideShow> GetEnableOrDisables(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _slideShowRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetEnableOrDisables");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var slideShows = _cacheManager.GetCollection<SlideShow>(key);
            if (slideShows == null)
            {
                slideShows = _slideShowRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
                _cacheManager.Put(key, slideShows);
            }

            return slideShows;
        }

        public IEnumerable<SlideShow> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _slideShowRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}