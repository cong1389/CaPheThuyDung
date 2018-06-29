using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Menu;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Menu
{
    public interface IMenuLinkRepository : IRepositoryBase<MenuLink>
	{
        MenuLink GetById(int id);

        IEnumerable<MenuLink> PagedList(Paging page);

		IEnumerable<MenuLink> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}