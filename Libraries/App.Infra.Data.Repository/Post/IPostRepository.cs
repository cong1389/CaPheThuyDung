using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Post
{
	public interface IPostRepository : IRepositoryBase<Domain.Entities.Data.Post>
	{
		Domain.Entities.Data.Post GetById(int id);

		IEnumerable<Domain.Entities.Data.Post> PagedList(Paging page);

		IEnumerable<Domain.Entities.Data.Post> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);

		IEnumerable<Domain.Entities.Data.Post> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page);
	}
}