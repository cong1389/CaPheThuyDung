using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.GenericControl;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.GenericControl
{
    public class GenericControlValueRepository : RepositoryBase<GenericControlValue>, IGenericControlValueRepository
	{
		public GenericControlValueRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public GenericControlValue GetById(int id)
		{
			var genericControlValue = FindBy(x => x.Id == id).FirstOrDefault();
			return genericControlValue;
		}

		protected override IOrderedQueryable<GenericControlValue> GetDefaultOrder(IQueryable<GenericControlValue> query)
		{
			var genericControlValues = 
				from p in query
				orderby p.Id
				select p;
			return genericControlValues;
		}

		public IEnumerable<GenericControlValue> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<GenericControlValue> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<GenericControlValue>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.ValueName.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.GenericControl.Name.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}