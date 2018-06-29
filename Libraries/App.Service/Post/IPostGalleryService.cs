using System.Collections.Generic;
using App.Domain.Entities.Data;
using App.Domain.Interfaces.Services;

namespace App.Service.Post
{
	public interface IPostGalleryService : IBaseService<PostGallery>
	{
        PostGallery GetById(int id, bool isCache = true);

        IEnumerable<PostGallery> GetByPostId(int postId, bool isCache = true);

        int GetMaxOrderDiplay(int postId);
    }
}