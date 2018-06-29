using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using App.Aplication;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Ads;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Ads;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Ads
{
    public class BannerService : BaseService<Banner>, IBannerService
    {
        private const string CacheKey = "db.Banner.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IBannerRepository _bannerRepository;

        public BannerService(IUnitOfWork unitOfWork, IBannerRepository bannerRepository, ICacheManager cacheManager) : base(unitOfWork, bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _cacheManager = cacheManager;
        }

        public Banner GetById(int id, bool isCache = true)
        {
            Banner banner;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                banner = _cacheManager.Get<Banner>(key);
                if (banner == null)
                {
                    banner = _bannerRepository.GetById(id);
                    _cacheManager.Put(key, banner);
                }
            }
            else
            {
                banner = _bannerRepository.GetById(id);
            }

            return banner;
        }

        public Banner GetBanner(int? menuId = null, int status = 1, List<int> position = null, bool isCache = true)
        {
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetBanner");

            var expression = PredicateBuilder.True<Banner>();
            expression = expression.And(x => x.Status == status);
            sbKey.AppendFormat("-{0}", status);

            if (menuId != null)
            {
                sbKey.AppendFormat("-{0}", menuId);
                expression = expression.And(x => x.MenuId == menuId);
            }

            if (position.IsAny())
            {
                var i = 0;
                foreach (var pos in position)
                {
                    sbKey.AppendFormat("-{0}", pos);
                    expression = i == 0 ? expression.And(x => x.PageBanner.Position == pos) : expression.Or(x => x.PageBanner.Position == pos);
                    i++;
                }
            }

            //Where from date and to date
            expression = expression.And(x => (!x.FromDate.HasValue || DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
                                             (!x.ToDate.HasValue || DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            if (!isCache)
            {
                return _bannerRepository.Get(expression);
            }

            var key = sbKey.ToString();
            var banner = _cacheManager.Get<Banner>(key);
            if (banner == null)
            {
                banner = _bannerRepository.Get(expression);
                _cacheManager.Put(key, banner);
            }

            return banner;
        }

        public IEnumerable<Banner> GetBanners(int? menuId = null, int status = 1, List<int> position = null, bool isCache = true)
        {
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheKey, "GetBanners");

            var expression = PredicateBuilder.True<Banner>();
            expression = expression.And(x => x.Status == status);
            sbKey.AppendFormat("-{0}", status);

            if (menuId != null)
            {
                sbKey.AppendFormat("-{0}", menuId);
                expression = expression.And(x => x.MenuId == menuId);
            }

            if (position.IsAny())
            {
                var i = 0;
                foreach (var pos in position)
                {
                    sbKey.AppendFormat("-{0}", pos);
                    expression = i == 0 ? expression.And(x => x.PageBanner.Position == pos) : expression.Or(x => x.PageBanner.Position == pos);
                    i++;
                }
            }

            //Where from date and to date
            expression = expression.And(x => (!x.FromDate.HasValue || DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) >= 0) &&
                                        (!x.ToDate.HasValue || DbFunctions.DiffHours(x.ToDate.Value, DateTimeOffset.UtcNow.Offset) <= 0));

            if (!isCache)
            {
                return FindBy(expression);
            }

            var key = sbKey.ToString();
            var banners = _cacheManager.GetCollection<Banner>(key);
            if (banners == null)
            {
                _cacheManager.Put(key, banners);
                banners = FindBy(expression);

            }

            return banners;
        }

        public IEnumerable<Banner> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _bannerRepository.PagedSearchList(sortbuBuilder, page);
        }
    }
}