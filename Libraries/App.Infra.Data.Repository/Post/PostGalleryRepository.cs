using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Post
{
    public class PostGalleryRepository : RepositoryBase<PostGallery>, IPostGalleryRepository
	{

        public PostGalleryRepository(IDbFactory dbFactory) : base(dbFactory)
		{
        }

        public PostGallery GetById(int id)
        {
            var postGallery = FindBy(x => x.Id == id).FirstOrDefault();
            
            return postGallery;
        }

        protected override IOrderedQueryable<PostGallery> GetDefaultOrder(IQueryable<PostGallery> query)
        {
            var postGallery =
                from p in query
                orderby p.Id
                select p;
            return postGallery;
        }

        public IEnumerable<PostGallery> PagedList(Paging page)
        {
            return GetAllPagedList(page).ToList();
        }

        //public IEnumerable<App.Domain.Entities.Data.PostGallery> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
        //{
        //    Expression<Func<App.Domain.Entities.Data.PostGallery, bool>> expression = PredicateBuilder.True<App.Domain.Entities.Data.PostGallery>();
        //    if (!string.IsNullOrEmpty(sortBuider.Keywords))
        //    {
        //        expression = expression.And<App.Domain.Entities.Data.PostGallery>((App.Domain.Entities.Data.PostGallery x) => x.PostId.Contains(sortBuider.Keywords.ToLower()) 
        //        );
        //    }
        //    return this.FindAndSort(expression, sortBuider.Sorts, page);
        //}

        //public IEnumerable<App.Domain.Entities.Data.PostGallery> PagedSearchListByMenu(SortingPagingBuilder sortBuider, Paging page)
        //{
        //    Expression<Func<App.Domain.Entities.Data.PostGallery, bool>> expression = PredicateBuilder.True<App.Domain.Entities.Data.PostGallery>();
        //    if (!string.IsNullOrEmpty(sortBuider.Keywords))
        //    {
        //        expression = expression.And<App.Domain.Entities.Data.PostGallery>((App.Domain.Entities.Data.PostGallery x) => x.VirtualCategoryId.Contains(sortBuider.Keywords) && x.Status == 1);
        //    }
        //    return this.FindAndSort(expression, sortBuider.Sorts, page);
        //}
    }
}