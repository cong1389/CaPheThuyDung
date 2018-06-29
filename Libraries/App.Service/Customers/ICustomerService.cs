using System;
using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Interfaces.Services;
using Domain.Entities.Customers;

namespace App.Service.Customers
{
    public interface ICustomerService : IBaseService<Customer>
    {
        Customer GetById(int id, bool isCache = true);

        Customer GetByGuid(Guid guid, bool isCache = true);

        Customer InsertGuestCusomter(Guid? customerGuid = null);

        IEnumerable<Customer> PagedList(SortingPagingBuilder sortBuider, Paging page);

        void ResetCheckoutData(Customer customer, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true);

        void UpdateCustomer(Customer customer);

    }
}