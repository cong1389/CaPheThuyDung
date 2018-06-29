using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Data;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Repairs
{
    public class RepairRepository : RepositoryBase<Repair>, IRepairRepository
	{
		public RepairRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<Repair> GetDefaultOrder(IQueryable<Repair> query)
		{
			var orders = 
				from p in query
				orderby p.Id
				select p;
			return orders;
		}

		public IEnumerable<Repair> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Repair> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Repair>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.RepairCode.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.CustomerCode.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.CustomerName.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.CreatedDate.ToString("dd/MM/yyyy").ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}