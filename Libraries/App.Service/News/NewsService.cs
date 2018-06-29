using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Extensions;
using App.Core.Utilities;
using App.Domain.Common;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.News;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.News
{
    public class NewsService : BaseService<Domain.Entities.Data.News>, INewsService
    {
        private const string CacheKey = "db.News.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly INewsRepository _newsRepository;

        public NewsService(IUnitOfWork unitOfWork, INewsRepository newsRepository, ICacheManager cacheManager) : base(unitOfWork, newsRepository)
        {
            _newsRepository = newsRepository;
            _cacheManager = cacheManager;
        }

        public Domain.Entities.Data.News GetById(int id, bool isCache = true)
        {
            Domain.Entities.Data.News news;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                news = _cacheManager.Get<Domain.Entities.Data.News>(key);
                if (news == null)
                {
                    news = _newsRepository.GetById(id);
                    _cacheManager.Put(key, news);
                }
            }
            else
            {
                news = _newsRepository.GetById(id);

            }
            
            return news;
        }

        public IEnumerable<Domain.Entities.Data.News> GetBySeoUrl(string seoUrl, bool isCache = true)
        {
            IEnumerable<Domain.Entities.Data.News> news;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetBySeoUrls");

                if (seoUrl.HasValue())
                {
                    sbKey.AppendFormat("-{0}", seoUrl);
                }

                var key = sbKey.ToString();
                news = _cacheManager.GetCollection<Domain.Entities.Data.News>(key);
                if (news == null)
                {
                    news = _newsRepository.FindBy(x => x.SeoUrl.Equals(seoUrl));
                    _cacheManager.Put(key, news);
                }
            }
            else
            {
                news = _newsRepository.FindBy(x => x.SeoUrl.Equals(seoUrl));
            }

            return news;
        }

        public IEnumerable<Domain.Entities.Data.News> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _newsRepository.PagedSearchList(sortbuBuilder, page);
        }

        public IEnumerable<Domain.Entities.Data.News> PagedListByMenu(SortingPagingBuilder sortBuider, Paging page)
        {
            return _newsRepository.PagedSearchListByMenu(sortBuider, page);
        }

        public IEnumerable<Domain.Entities.Data.News> GetByOption(string virtualCategoryId = null
           , bool? isDisplayHomePage = null
           , bool? isVideo = null
           , int status = 1
           , bool isCache = true)
        {
            IEnumerable<Domain.Entities.Data.News> news;
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetByOption");

            var expression = PredicateBuilder.True<Domain.Entities.Data.News>();
            sbKey.AppendFormat("-{0}", status);
            expression = expression.And(x => x.Status == status);

            if (virtualCategoryId.HasValue())
            {
                sbKey.AppendFormat("-{0}", virtualCategoryId);
                expression = expression.And(x => x.VirtualCategoryId.Contains(virtualCategoryId));
            }
            if (isDisplayHomePage != null)
            {
                sbKey.AppendFormat("-{0}", isDisplayHomePage);
                expression = expression.And(x => x.HomeDisplay == isDisplayHomePage);
            }
            if (isVideo != null)
            {
                sbKey.AppendFormat("-{0}", isVideo);
                expression = expression.And(x => x.Video == isVideo);
            }

            if (isCache)
            {
                var key = sbKey.ToString();
                news = _cacheManager.GetCollection<Domain.Entities.Data.News>(key);
                if (news == null)
                {
                    news = FindBy(expression);
                    _cacheManager.Put(key, news);
                }
            }
            else
            {
                news = FindBy(expression);
            }

            return news;
        }

        public IEnumerable<Domain.Entities.Data.News> GetEnableOrDisables(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _newsRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetEnableOrDisables");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var news = _cacheManager.GetCollection<Domain.Entities.Data.News>(key);
            if (news == null)
            {
                news = _newsRepository.FindBy(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable), true);
                _cacheManager.Put(key, news);
            }

            return news;
        }

        public Domain.Entities.Data.News GetEnableOrDisable(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _newsRepository.Get(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable));
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetEnableOrDisable");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var newses = _cacheManager.Get<Domain.Entities.Data.News>(key);
            if (newses == null)
            {
                newses = _newsRepository.Get(x => x.Status == (enable ? (int)Status.Enable : (int)Status.Disable));
                _cacheManager.Put(key, newses);
            }

            return newses;
        }
    }
}