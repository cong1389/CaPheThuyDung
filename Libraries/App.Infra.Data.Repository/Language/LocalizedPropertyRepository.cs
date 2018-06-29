using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Language;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Language
{
    public class LocalizedPropertyRepository : RepositoryBase<LocalizedProperty>, ILocalizedPropertyRepository
	{
        public LocalizedPropertyRepository(IDbFactory dbFactory) : base(dbFactory)
		{
        }

		protected override IOrderedQueryable<LocalizedProperty> GetDefaultOrder(IQueryable<LocalizedProperty> query)
		{
			var languages = 
				from p in query
				orderby p.Id
				select p;
			return languages;
		}

		public LocalizedProperty GetId(int id)
		{
            var language = FindBy(x => x.Id == id).FirstOrDefault();

            return language;
		}

		public IEnumerable<LocalizedProperty> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<LocalizedProperty> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<LocalizedProperty>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.LocaleKey.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.LocaleKey.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}