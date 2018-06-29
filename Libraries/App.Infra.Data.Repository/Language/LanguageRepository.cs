using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Language
{
    public class LanguageRepository : RepositoryBase<Domain.Entities.Language.Language>, ILanguageRepository
	{
        public LanguageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

		protected override IOrderedQueryable<Domain.Entities.Language.Language> GetDefaultOrder(IQueryable<Domain.Entities.Language.Language> query)
		{
			var languages = 
				from p in query
				orderby p.Id
				select p;
			return languages;
		}

		public Domain.Entities.Language.Language GetById(int id)
		{
            var language = FindBy(x => x.Id == id).FirstOrDefault();

            return language;
		}

		public IEnumerable<Domain.Entities.Language.Language> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Domain.Entities.Language.Language> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.Language.Language>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.LanguageCode.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.LanguageName.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}