using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Extensions;
using App.Core.Utilities;
using App.Domain.Entities.Menu;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Menu;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.Menu
{
    public class MenuLinkService : BaseService<MenuLink>, IMenuLinkService
    {
        private const string CacheMenuKey = "db.MenuLink.{0}";
        private const string CacheMenuKey2 = "db.MenuLink.{0}-{1}-{2}";
        private readonly ICacheManager _cacheManager;
        private readonly IMenuLinkRepository _menuLinkRepository;

        public MenuLinkService(IUnitOfWork unitOfWork, IMenuLinkRepository menuLinkRepository
            , ICacheManager cacheManager) : base(unitOfWork, menuLinkRepository)
        {
            _menuLinkRepository = menuLinkRepository;
            _cacheManager = cacheManager;
        }

        public MenuLink GetMenu(int id, bool isCache = true)
        {
            MenuLink menuLink;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheMenuKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                menuLink = _cacheManager.Get<MenuLink>(key);
                if (menuLink == null)
                {
                    menuLink = _menuLinkRepository.GetById(id);
                    _cacheManager.Put(key, menuLink);
                }
            }
            else
            {
                menuLink = _menuLinkRepository.GetById(id);
            }

            return menuLink;
        }

        public IEnumerable<MenuLink> GetListSeoUrl(string seoUrl, bool isCache = true)
        {
            IEnumerable<MenuLink> menuLinks;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheMenuKey, "GetListSeoUrl");

                if (seoUrl.HasValue())
                {
                    sbKey.AppendFormat("-{0}", seoUrl);
                }

                var key = sbKey.ToString();
                menuLinks = _cacheManager.GetCollection<MenuLink>(key);
                if (menuLinks == null)
                {
                    menuLinks = _menuLinkRepository.FindBy(x => x.SeoUrl.Equals(seoUrl));
                    _cacheManager.Put(key, menuLinks);
                }
            }
            else
            {
                menuLinks = _menuLinkRepository.FindBy(x => x.SeoUrl.Equals(seoUrl));

            }

            return menuLinks;
        }

        public MenuLink GetBySeoUrl(string seoUrl, bool @readonly = false, bool isCache = true)
        {
            MenuLink menuLink;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheMenuKey, "GetBySeoUrls");

                if (seoUrl.HasValue())
                {
                    sbKey.AppendFormat("-{0}", seoUrl);
                }

                var key = sbKey.ToString();
                menuLink = _cacheManager.Get<MenuLink>(key);
                if (menuLink == null)
                {
                    menuLink = _menuLinkRepository.Get(x => x.SeoUrl.Equals(seoUrl), @readonly);
                    _cacheManager.Put(key, menuLink);
                }
            }
            else
            {
                menuLink = _menuLinkRepository.Get(x => x.SeoUrl.Equals(seoUrl), @readonly);
            }

            return menuLink;
        }

        public MenuLink GetByParentId(int parentId, string currentVirtualId, bool isCache = true)
        {
            MenuLink menuLink;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheMenuKey, "GetByParentId");
                sbKey.AppendFormat("-{0}", parentId);

                if (currentVirtualId.HasValue())
                {
                    sbKey.AppendFormat("-{0}", currentVirtualId);
                }

                var key = sbKey.ToString();
                menuLink = _cacheManager.Get<MenuLink>(key);
                if (menuLink == null)
                {
                    menuLink = _menuLinkRepository.Get(x => x.CurrentVirtualId.Equals(currentVirtualId) && x.ParentId != parentId);
                    _cacheManager.Put(key, menuLink);
                }
            }
            else
            {
                menuLink = _menuLinkRepository.Get(x => x.CurrentVirtualId.Equals(currentVirtualId) && x.ParentId != parentId);
            }

            return menuLink;
        }

        public MenuLink GetByCurrentVirtualId(string currentVirtualId, bool isCache = true)
        {
            MenuLink menuLink;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheMenuKey, "GetByCurrentVirtualId");

                if (currentVirtualId.HasValue())
                {
                    sbKey.AppendFormat("-{0}", currentVirtualId);
                }

                var key = sbKey.ToString();
                menuLink = _cacheManager.Get<MenuLink>(key);
                if (menuLink == null)
                {
                    menuLink = _menuLinkRepository.Get(x => x.CurrentVirtualId.Equals(currentVirtualId));
                    _cacheManager.Put(key, menuLink);
                }
            }
            else
            {
                menuLink = _menuLinkRepository.Get(x => x.CurrentVirtualId.Equals(currentVirtualId));
            }


            return menuLink;
        }

        public MenuLink GetByMenuName(string virtualCategoryId, string menuName, bool isCache = true)
        {
            MenuLink menuLink;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheMenuKey, "GetByMenuName");

                if (virtualCategoryId.HasValue())
                {
                    sbKey.AppendFormat("-{0}", virtualCategoryId);
                }

                if (menuName.HasValue())
                {
                    sbKey.AppendFormat("-{0}", menuName);
                }

                var key = sbKey.ToString();
                menuLink = _cacheManager.Get<MenuLink>(key);
                if (menuLink == null)
                {
                    menuLink = _menuLinkRepository.Get(x => x.CurrentVirtualId.Equals(virtualCategoryId) && !x.MenuName.Equals(menuName));
                    _cacheManager.Put(key, menuLink);
                }
            }
            else
            {
                menuLink = _menuLinkRepository.Get(x => x.CurrentVirtualId.Equals(virtualCategoryId) && !x.MenuName.Equals(menuName));
            }

            return menuLink;
        }

        public IEnumerable<MenuLink> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _menuLinkRepository.PagedSearchList(sortbuBuilder, page);
        }

        public IEnumerable<MenuLink> GetByTemplateType(int templateType, bool @readonly = false, bool isCache = true)
        {
            IEnumerable<MenuLink> menuLinks;
            if (isCache)
            {
                var key = string.Format(CacheMenuKey2, "GetByTemplateType", templateType, @readonly);

                menuLinks = _cacheManager.GetCollection<MenuLink>(key);
                if (menuLinks == null)
                {
                    menuLinks = _menuLinkRepository.FindBy(x => x.TemplateType == templateType, @readonly);
                    _cacheManager.Put(CacheMenuKey2, menuLinks);
                }
            }
            else
            {
                menuLinks = _menuLinkRepository.FindBy(x => x.TemplateType == templateType, @readonly);
            }

            return menuLinks;
        }

        public IEnumerable<MenuLink> GetByOptions(List<int> position = null
            , List<int> template = null
            , string virtualId = null
            , string seoUrl = null
            , List<int> parentId = null
            , bool? isDisplayHomePage = null
            , bool? isDisplaySearch = null
            , int status = 1
            , int? id = null
            , bool isCache = true)
        {

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheMenuKey, "GetByOption");

            var expression = PredicateBuilder.True<MenuLink>();
            sbKey.AppendFormat("-{0}", status);
            expression = expression.And(x => x.Status == status);

            if (position != null && !position.IsNullOrEmpty())
            {
                var i = 0;
                foreach (var pos in position)
                {
                    sbKey.AppendFormat("-{0}", pos);
                    expression = i == 0 ? expression.And(x => x.Position == pos) : expression.Or(x => x.Position == pos);
                    i++;
                }
            }

            if (template != null && !template.IsNullOrEmpty())
            {
                foreach (var temp in template)
                {
                    sbKey.AppendFormat("-{0}", temp);
                    var i = 0;
                    expression = i == 0
                        ? expression.And(x => x.TemplateType == temp)
                        : expression.Or(x => x.TemplateType == temp);
                    i++;
                }
            }

            if (parentId != null && !parentId.IsNullOrEmpty())
            {
                var i = 0;
                foreach (var par in parentId)
                {
                    sbKey.AppendFormat("-{0}", parentId);
                    expression = i == 0 ? expression.And(x => x.ParentId == par) : expression.Or(x => x.ParentId == par);
                    i++;
                }
            }

            if (virtualId != null && virtualId.HasValue())
            {
                sbKey.AppendFormat("-{0}", virtualId);
                expression = expression.And(x => x.VirtualId.Contains(virtualId));
            }

            if (seoUrl != null && seoUrl.HasValue())
            {
                sbKey.AppendFormat("-{0}", seoUrl);
                expression = expression.And(x => x.SeoUrl.Equals(seoUrl));
            }

            if (isDisplayHomePage != null)
            {
                //isDisplayHomePage
                sbKey.AppendFormat("-{0}", isDisplayHomePage);
                expression = expression.And(x => x.DisplayOnHomePage == isDisplayHomePage);
            }

            if (isDisplaySearch != null)
            {
                //isDisplaySearch
                sbKey.AppendFormat("-{0}", isDisplaySearch);
                expression = expression.And(x => x.DisplayOnSearch == isDisplaySearch);
            }

            if (id != null)
            {
                sbKey.AppendFormat("-{0}", id);
                expression = expression.And(x => x.Id == id);
            }

            IEnumerable<MenuLink> menuLinks;
            if (isCache)
            {

                var key = sbKey.ToString();
                menuLinks = _cacheManager.GetCollection<MenuLink>(key);
                if (menuLinks == null)
                {
                    menuLinks = FindBy(expression);
                    _cacheManager.Put(key, menuLinks);
                }
            }
            else
            {
                menuLinks = FindBy(expression);
            }

            return menuLinks;
        }

        public IEnumerable<MenuLink> GetEnableOrDisables(bool enable = true, bool isCache = true)
        {
            if (!isCache)
            {
                return _menuLinkRepository.FindBy(x => x.Status == (enable ? 1 : 0), true);
            }

            var sbKey = new StringBuilder();
            sbKey.AppendFormat(CacheMenuKey2, "GetEnableOrDisables");
            sbKey.Append(enable);

            var key = sbKey.ToString();

            var slideShows = _cacheManager.GetCollection<MenuLink>(key);
            if (slideShows == null)
            {
                slideShows = _menuLinkRepository.FindBy(x => x.Status == (enable ? 1 : 0), true);
                _cacheManager.Put(key, slideShows);
            }

            return slideShows;
        }
    }
}