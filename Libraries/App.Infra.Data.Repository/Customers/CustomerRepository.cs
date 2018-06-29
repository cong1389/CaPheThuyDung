using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;
using Domain.Entities.Customers;

namespace App.Infra.Data.Repository.Customers
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
	{
		public CustomerRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public Customer GetById(int id)
		{
			var province = FindBy(x => x.Id == id).FirstOrDefault();
			return province;
		}

		protected override IOrderedQueryable<Customer> GetDefaultOrder(IQueryable<Customer> query)
		{
			var customer = 
				from p in query
				orderby p.Id
				select p;
			return customer;
		}

		public IEnumerable<Customer> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Customer> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Customer>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Username.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Email.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}