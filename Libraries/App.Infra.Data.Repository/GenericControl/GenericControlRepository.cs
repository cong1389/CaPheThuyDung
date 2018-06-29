using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.GenericControl
{
    public class GenericControlRepository : RepositoryBase<Domain.Entities.GenericControl.GenericControl>, IGenericControlRepository
	{
		public GenericControlRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Domain.Entities.GenericControl.GenericControl GetById(int id)
		{
			var genericControl = FindBy(x => x.Id == id).FirstOrDefault();
			return genericControl;
		}

		protected override IOrderedQueryable<Domain.Entities.GenericControl.GenericControl> GetDefaultOrder(IQueryable<Domain.Entities.GenericControl.GenericControl> query)
		{
			var genericControls = 
				from p in query
				orderby p.Id
				select p;
			return genericControls;
		}

		public IEnumerable<Domain.Entities.GenericControl.GenericControl> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Domain.Entities.GenericControl.GenericControl> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Domain.Entities.GenericControl.GenericControl>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Name.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Description.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}