using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Attribute;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Attribute
{
    public class AttributeValueRepository : RepositoryBase<AttributeValue>, IAttributeValueRepository
	{
		public AttributeValueRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public AttributeValue GetById(int id)
		{
			var attributeValue = FindBy(x => x.Id == id).FirstOrDefault();
			return attributeValue;
		}

		protected override IOrderedQueryable<AttributeValue> GetDefaultOrder(IQueryable<AttributeValue> query)
		{
			var attributeValues = 
				from p in query
				orderby p.Id
				select p;
			return attributeValues;
		}

		public IEnumerable<AttributeValue> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<AttributeValue> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<AttributeValue>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.ValueName.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Attribute.AttributeName.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}