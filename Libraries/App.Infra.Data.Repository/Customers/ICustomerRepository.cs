using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Repository;
using Domain.Entities.Customers;

namespace App.Infra.Data.Repository.Customers
{
	public interface ICustomerRepository : IRepositoryBase<Customer>
	{
		Customer GetById(int id);

		IEnumerable<Customer> PagedList(Paging page);

		IEnumerable<Customer> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}