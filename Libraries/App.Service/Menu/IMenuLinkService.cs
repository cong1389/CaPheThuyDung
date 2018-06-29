using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Menu;
using App.Domain.Interfaces.Services;

namespace App.Service.Menu
{
    public interface IMenuLinkService : IBaseService<MenuLink>
    {
        MenuLink GetMenu(int id, bool isCache = true);

        IEnumerable<MenuLink> GetListSeoUrl(string seoUrl, bool isCache = true);

        IEnumerable<MenuLink> GetByTemplateType(int templateType, bool @readonly = false, bool isCache = true);

        MenuLink GetBySeoUrl(string seoUrl, bool @readonly = false, bool isCache = true);

        MenuLink GetByParentId(int parentId, string currentVirtualId, bool isCache = true);

        MenuLink GetByCurrentVirtualId(string currentVirtualId, bool isCache = true);

        MenuLink GetByMenuName(string virtualCategoryId, string menuName, bool isCache = true);

        IEnumerable<MenuLink> PagedList(SortingPagingBuilder sortBuider, Paging page);

        IEnumerable<MenuLink> GetByOptions(List<int> position = null
                , List<int> template = null
                , string virtualId = null
                , string seoUrl = null
             , List<int> parentId = null
                , bool? isDisplayHomePage = null
                , bool? isDisplaySearch = null
                , int status = 1
            , int? id = null
            , bool isCache = true);

        IEnumerable<MenuLink> GetEnableOrDisables(bool enable = true, bool isCache = true);
    }
}