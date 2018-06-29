using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Core.Utilities;
using App.Domain.Common;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Addresses
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
	{
        public AddressRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

		public Address GetById(int id)
		{
            var address = FindBy(x => x.Id == id).FirstOrDefault();

            return address;
		}

		protected override IOrderedQueryable<Address> GetDefaultOrder(IQueryable<Address> query)
		{
			var address = 
				from p in query
				orderby p.Id
				select p;
			return address;
		}

		public IEnumerable<Address> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Address> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Address>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.Title.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.Address1.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}