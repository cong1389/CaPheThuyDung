using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Post
{
	public interface IPostGalleryRepository : IRepositoryBase<PostGallery>
	{
        PostGallery GetById(int id);

        IEnumerable<PostGallery> PagedList(Paging page);

        //IEnumerable<PostGallery> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);

        //IEnumerable<PostGallery> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page);
    }
}