using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Menu;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Menu
{
    public class MenuLinkRepository : RepositoryBase<MenuLink>, IMenuLinkRepository
    {
        public MenuLinkRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public MenuLink GetById(int id)
        {
            var menuLink = FindBy(x => x.Id == id).FirstOrDefault();         

            return menuLink;
        }

        protected override IOrderedQueryable<MenuLink> GetDefaultOrder(IQueryable<MenuLink> query)
        {
            var menuLinks =
                from p in query
                orderby p.Id
                select p;
            return menuLinks;
        }

        public IEnumerable<MenuLink> PagedList(Paging page)
        {
            return GetAllPagedList(page).ToList();
        }

        public IEnumerable<MenuLink> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
        {
            var expression = PredicateBuilder.True<MenuLink>();
            if (!string.IsNullOrEmpty(sortBuider.Keywords))
            {
                expression = expression.And(x => x.MenuName.ToLower().Contains(sortBuider.Keywords.ToLower()));
            }
            return FindAndSort(expression, sortBuider.Sorts, page);
        }
    }
}