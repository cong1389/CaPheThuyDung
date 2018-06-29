using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Attribute
{
    public class AttributeRepository : RepositoryBase<Domain.Entities.Attribute.Attribute>, IAttributeRepository
	{
		public AttributeRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Domain.Entities.Attribute.Attribute GetById(int id)
		{
			var attribute = FindBy(x => x.Id == id).FirstOrDefault();
			return attribute;
		}

		protected override IOrderedQueryable<Domain.Entities.Attribute.Attribute> GetDefaultOrder(IQueryable<Domain.Entities.Attribute.Attribute> query)
		{
			var attributes = 
				from p in query
				orderby p.Id
				select p;
			return attributes;
		}

		public IEnumerable<Domain.Entities.Attribute.Attribute> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Domain.Entities.Attribute.Attribute> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.Attribute.Attribute>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.AttributeName.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}