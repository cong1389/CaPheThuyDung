using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Payments;
using App.Domain.Interfaces.Services;

namespace App.Service.PaymentMethodes
{
    public interface IPaymentMethodService : IBaseService<PaymentMethod>
	{
		PaymentMethod GetById(int id);

	    IEnumerable<PaymentMethod> GetAll(bool isCache = true);

        PaymentMethod GetBySystemName(string systemName);

        IEnumerable<PaymentMethod> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}