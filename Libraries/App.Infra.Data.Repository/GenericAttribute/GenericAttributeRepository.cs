using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.GenericAttribute
{
    public class GenericAttributeRepository : RepositoryBase<Domain.Entities.Data.GenericAttribute>, IGenericAttributeRepository
	{
		public GenericAttributeRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

        public Domain.Entities.Data.GenericAttribute GetAttributeById(int id)
        {
            var attribute = FindBy(x => x.Id == id).FirstOrDefault();
            return attribute;
        }

        protected override IOrderedQueryable<Domain.Entities.Data.GenericAttribute> GetDefaultOrder(IQueryable<Domain.Entities.Data.GenericAttribute> query)
        {
            var attributes =
                from p in query
                orderby p.Id
                select p;
            return attributes;
        }

        public IEnumerable<Domain.Entities.Data.GenericAttribute> PagedList(Paging page)
        {
            return GetAllPagedList(page).ToList();
        }

        public IEnumerable<Domain.Entities.Data.GenericAttribute> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
        {
            var expression = PredicateBuilder.True<Domain.Entities.Data.GenericAttribute>();
            if (!string.IsNullOrEmpty(sortBuider.Keywords))
            {
                expression = expression.And(x => x.Key.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Value.ToLower().Contains(sortBuider.Keywords.ToLower()));
            }
            return FindAndSort(expression, sortBuider.Sorts, page);
        }
    }
}