using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.News
{
    public class NewsRepository : RepositoryBase<Domain.Entities.Data.News>, INewsRepository
	{
		public NewsRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Domain.Entities.Data.News GetById(int id)
		{
			var news = FindBy(x => x.Id == id).FirstOrDefault();
			return news;
		}

		protected override IOrderedQueryable<Domain.Entities.Data.News> GetDefaultOrder(IQueryable<Domain.Entities.Data.News> query)
		{
			var news = 
				from p in query
				orderby p.Id
				select p;
			return news;
		}

		public IEnumerable<Domain.Entities.Data.News> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Domain.Entities.Data.News> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.Data.News>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Title.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}

		public IEnumerable<Domain.Entities.Data.News> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.Data.News>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.VirtualCategoryId.Contains(sortBuider.Keywords) && x.Status == 1);
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}