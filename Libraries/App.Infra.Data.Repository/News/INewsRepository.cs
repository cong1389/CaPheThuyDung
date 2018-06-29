using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.News
{
	public interface INewsRepository : IRepositoryBase<Domain.Entities.Data.News>
	{
		Domain.Entities.Data.News GetById(int id);

		IEnumerable<Domain.Entities.Data.News> PagedList(Paging page);

		IEnumerable<Domain.Entities.Data.News> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);

		IEnumerable<Domain.Entities.Data.News> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page);
	}
}