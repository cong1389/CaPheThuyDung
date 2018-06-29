using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Ads;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Ads
{
    public class PageBannerService : BaseService<PageBanner>, IPageBannerService
	{
        private const string CachePagebannerKey = "db.PageBanner.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IPageBannerRepository _pageBannerRepository;

	    public PageBannerService(IUnitOfWork unitOfWork, IPageBannerRepository pageBannerRepository
            , ICacheManager cacheManager) : base(unitOfWork, pageBannerRepository)
		{
		    _pageBannerRepository = pageBannerRepository;
            _cacheManager = cacheManager;
        }

		public PageBanner GetById(int id,bool isCache = true)
        {
            PageBanner pageBanner;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CachePagebannerKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                pageBanner = _cacheManager.Get<PageBanner>(key);
                if (pageBanner == null)
                {
                    pageBanner = _pageBannerRepository.GetById(id);
                    _cacheManager.Put(key, pageBanner);
                }
            }
            else
            {
                pageBanner = _pageBannerRepository.GetById(id);
            }           

            return pageBanner;
		}

		public IEnumerable<PageBanner> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _pageBannerRepository.PagedSearchList(sortbuBuilder, page);
		}
	}
}