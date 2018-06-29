using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Slide;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Slide
{
    public class SlideShowRepository : RepositoryBase<SlideShow>, ISlideShowRepository
	{
		public SlideShowRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<SlideShow> GetDefaultOrder(IQueryable<SlideShow> query)
		{
			var slideShows = 
				from p in query
				orderby p.Id
				select p;
			return slideShows;
		}

		public IEnumerable<SlideShow> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<SlideShow> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<SlideShow>();
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}