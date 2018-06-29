using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Static
{
    public class StaticContentRepository : RepositoryBase<StaticContent>, IStaticContentRepository
	{
        public StaticContentRepository(IDbFactory dbFactory) : base(dbFactory)
		{
        }

		public StaticContent GetById(int id)
        {
            var staticContent = FindBy(x => x.Id == id).FirstOrDefault();
            
            return staticContent;
		}

		protected override IOrderedQueryable<StaticContent> GetDefaultOrder(IQueryable<StaticContent> query)
		{
			var staticContents = 
				from p in query
				orderby p.Id
				select p;
			return staticContents;
		}

		public IEnumerable<StaticContent> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<StaticContent> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<StaticContent>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Title.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}

		public IEnumerable<StaticContent> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<StaticContent>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.VirtualCategoryId.Contains(sortBuider.Keywords) && x.Status == 1);
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}