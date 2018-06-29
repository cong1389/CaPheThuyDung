using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.Payments;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.PaymentMethodes
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
	{
		public PaymentMethodRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public PaymentMethod GetById(int id)
		{
			var province = FindBy(x => x.Id == id).FirstOrDefault();
			return province;
		}

		protected override IOrderedQueryable<PaymentMethod> GetDefaultOrder(IQueryable<PaymentMethod> query)
		{
			var paymentMethod = 
				from p in query
				orderby p.Id
				select p;
			return paymentMethod;
		}

		public IEnumerable<PaymentMethod> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<PaymentMethod> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<PaymentMethod>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.PaymentMethodSystemName.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}