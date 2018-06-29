using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.LocaleStringResource
{
    public class LocaleStringResourceRepository : RepositoryBase<Domain.Entities.Language.LocaleStringResource>, ILocaleStringResourceRepository
	{

        public LocaleStringResourceRepository(IDbFactory dbFactory) : base(dbFactory)
		{
        }

        public Domain.Entities.Language.LocaleStringResource GetLocaleStringResourceById(int id)
        {
            var localeString = FindBy(x => x.Id == id).FirstOrDefault();
            
            return localeString;
        }

        protected override IOrderedQueryable<Domain.Entities.Language.LocaleStringResource> GetDefaultOrder(IQueryable<Domain.Entities.Language.LocaleStringResource> query)
        {
            var attributes =
                from p in query
                orderby p.Id
                select p;
            return attributes;
        }

        public IEnumerable<Domain.Entities.Language.LocaleStringResource> PagedList(Paging page)
        {
            return GetAllPagedList(page).ToList();
        }

        public IEnumerable<Domain.Entities.Language.LocaleStringResource> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
        {
            var expression = PredicateBuilder.True<Domain.Entities.Language.LocaleStringResource>();
            if (!string.IsNullOrEmpty(sortBuider.Keywords))
            {
                expression = expression.And(x => x.ResourceName.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.ResourceName.ToLower().Contains(sortBuider.Keywords.ToLower()));
            }
            return FindAndSort(expression, sortBuider.Sorts, page);
        }
    }
}