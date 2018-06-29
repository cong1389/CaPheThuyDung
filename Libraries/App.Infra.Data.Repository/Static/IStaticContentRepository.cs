using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Static
{
	public interface IStaticContentRepository : IRepositoryBase<StaticContent>
	{
		StaticContent GetById(int id);

		IEnumerable<StaticContent> PagedList(Paging page);

		IEnumerable<StaticContent> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);

		IEnumerable<StaticContent> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page);
	}
}