using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Payments;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.PaymentMethodes
{
    public interface IPaymentMethodRepository : IRepositoryBase<PaymentMethod>
	{
		PaymentMethod GetById(int id);

		IEnumerable<PaymentMethod> PagedList(Paging page);

		IEnumerable<PaymentMethod> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}