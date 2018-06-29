using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Aplication;
using App.Core.Caching;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Post;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Post
{
    public class PostGalleryService : BaseService<PostGallery>, IPostGalleryService
    {
        private const string CacheKey = "db.PostGallery.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IPostGalleryRepository _galleryRepository;

        public PostGalleryService(IUnitOfWork unitOfWork, IPostGalleryRepository galleryRepository
            , ICacheManager cacheManager) : base(unitOfWork, galleryRepository)
        {
            _galleryRepository = galleryRepository;
            _cacheManager = cacheManager;
        }

        public PostGallery GetById(int id, bool isCache = true)
        {
            PostGallery postGallery;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                postGallery = _cacheManager.Get<PostGallery>(key);
                if (postGallery == null)
                {
                    postGallery = _galleryRepository.GetById(id);
                    _cacheManager.Put(key, postGallery);
                }
            }
            else
            {
                postGallery = _galleryRepository.GetById(id);
            }

            return postGallery;
        }

        public IEnumerable<PostGallery> GetByPostId(int postId, bool isCache = true)
        {
            IEnumerable<PostGallery> postGallery;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheKey, "GetByPostId");
                sbKey.Append(postId);

                var key = sbKey.ToString();
                postGallery = _cacheManager.GetCollection<PostGallery>(key);
                if (postGallery == null)
                {
                    postGallery = _galleryRepository.FindBy(x => x.PostId == postId);
                    _cacheManager.Put(key, postGallery);
                }
            }
            else
            {
                postGallery = _galleryRepository.FindBy(x => x.PostId == postId);
            }

            return postGallery;
        }

        public int GetMaxOrderDiplay(int postId)
        {
            var postGallery = _galleryRepository.FindBy(x => x.PostId == postId);

            if (postGallery.IsAny())
            {
                int maxOrder = postGallery.OrderByDescending(x => x.OrderDisplay).FirstOrDefault().OrderDisplay;

                return maxOrder + 1;
            }

            return 0;
        }
    }
}