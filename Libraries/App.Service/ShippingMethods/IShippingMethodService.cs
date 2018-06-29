using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;
using App.Domain.Shippings;

namespace App.Service.ShippingMethodes
{
    public interface IShippingMethodService : IBaseService<ShippingMethod>
	{
		ShippingMethod GetById(int id);

		IEnumerable<ShippingMethod> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}