using System.Collections.Generic;
using App.Domain.Entities.Data;
using App.Domain.Interfaces.Services;

namespace App.Service.Gallery
{
    public interface IGalleryService : IBaseService<GalleryImage>
    {
        GalleryImage GetById(int id, bool isCache = true);

        IEnumerable<GalleryImage> GetByOption(int? attributeValueId = null
           , int? postId = null
           , int status = 1
            , bool isCache = true);

    }
}