using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Post
{
    public class PostRepository : RepositoryBase<Domain.Entities.Data.Post>, IPostRepository
	{

        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
		{
        }

	    public Domain.Entities.Data.Post GetById(int id)
        {
            var post = FindBy(x => x.Id == id).FirstOrDefault();
            
            return post;
		}

		protected override IOrderedQueryable<Domain.Entities.Data.Post> GetDefaultOrder(IQueryable<Domain.Entities.Data.Post> query)
		{
			var posts = 
				from p in query
				orderby p.Id
				select p;
			return posts;
		}

		public IEnumerable<Domain.Entities.Data.Post> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Domain.Entities.Data.Post> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.Data.Post>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x =>
				    x.Title.ToLower().Contains(sortBuider.Keywords.ToLower()) ||
				    x.ProductCode.Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}

		public IEnumerable<Domain.Entities.Data.Post> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.Data.Post>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.VirtualCategoryId.Contains(sortBuider.Keywords) && x.Status == 1);
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}