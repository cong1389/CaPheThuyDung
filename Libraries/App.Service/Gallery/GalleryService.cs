using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Gallery;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Gallery
{
    public class GalleryService : BaseService<GalleryImage>, IGalleryService
    {
        private const string CacheGalleryKey = "db.Gallery.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IUnitOfWork unitOfWork, IGalleryRepository galleryRepository
            , ICacheManager cacheManager) : base(unitOfWork, galleryRepository)
        {
            _galleryRepository = galleryRepository;
            _cacheManager = cacheManager;
        }

        public GalleryImage GetById(int id, bool isCache = true)
        {
            GalleryImage galleryImage;

            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheGalleryKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                galleryImage = _cacheManager.Get<GalleryImage>(key);
                if (galleryImage == null)
                {
                    galleryImage = _galleryRepository.GetGalleryById(id);
                    _cacheManager.Put(key, galleryImage);
                }
            }
            else
            {
                galleryImage = _galleryRepository.GetGalleryById(id);
            }

            return galleryImage;
        }

        public IEnumerable<GalleryImage> GetByOption(int? attributeValueId = null
            , int? postId = null
            , int status = 1
            , bool isCache = true)
        {
            IEnumerable<GalleryImage> galleryImages;
            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheGalleryKey, "GetByOption");

            var expression = PredicateBuilder.True<GalleryImage>();
            sbKey.AppendFormat("-{0}", status);
            expression = expression.And(x => x.Status == status);

            if (attributeValueId != null)
            {
                sbKey.AppendFormat("-{0}", attributeValueId);
                expression = expression.And(x => x.AttributeValueId == attributeValueId);
            }

            if (postId != null)
            {
                sbKey.AppendFormat("-{0}", postId);
                expression = expression.And(x => x.PostId == postId);
            }

            if (isCache)
            {
                var key = sbKey.ToString();
                galleryImages = _cacheManager.GetCollection<GalleryImage>(key);
                if (galleryImages == null)
                {
                    galleryImages = FindBy(expression);
                    _cacheManager.Put(key, galleryImages);
                }
            }
            else
            {
                galleryImages = FindBy(expression);
            }


            return galleryImages;
        }
    }
}