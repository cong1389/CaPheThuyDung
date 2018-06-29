using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Caching;
using App.Core.Utilities;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Customers;
using App.Infra.Data.UOW.Interfaces;
using Domain.Entities.Customers;

namespace App.Service.Customers
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private const string CacheCustomerKey = "db.Customer.{0}";
        private readonly ICacheManager _cacheManager;

        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, ICacheManager cacheManager) : base(unitOfWork, customerRepository)
        {
            _customerRepository = customerRepository;
            _cacheManager = cacheManager;
        }

        public Customer GetById(int id, bool isCache = true)
        {
            Customer customer;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheCustomerKey, "GetById");
                sbKey.Append(id);

                var key = sbKey.ToString();
                customer = _cacheManager.Get<Customer>(key);
                if (customer == null)
                {
                    customer = _customerRepository.GetById(id);
                    _cacheManager.Put(key, customer);
                }
            }
            else
            {
                customer = _customerRepository.GetById(id);
            }

            return customer;
        }

        public Customer GetByGuid(Guid guid, bool isCache = true)
        {
            Customer customer;
            if (isCache)
            {
                var sbKey = new StringBuilder();
                sbKey.AppendFormat(CacheCustomerKey, "GetByGuid");
                sbKey.Append(guid);

                var key = sbKey.ToString();
                customer = _cacheManager.Get<Customer>(key);
                if (customer == null)
                {
                    customer = _customerRepository.Get(x => x.CustomerGuid == guid);
                    _cacheManager.Put(key, customer);
                }
            }
            else
            {
                customer = _customerRepository.Get(x => x.CustomerGuid == guid);
            }

            return customer;
        }

        public Customer InsertGuestCusomter(Guid? customerGuid = null)
        {
            var customer = new Customer
            {
                CustomerGuid = customerGuid ?? Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow
            };

            return _customerRepository.Add(customer);
        }

        public IEnumerable<Customer> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
        {
            return _customerRepository.PagedSearchList(sortbuBuilder, page);
        }


        public virtual void ResetCheckoutData(Customer customer, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            if (clearShippingMethod)
            {
                //_genericAttributeService.SaveGenericAttribute(customer.Id,"Customer", Contains.SelectedShippingOption,null);
            }

            if (clearPaymentMethod)
            {
                //_genericAttributeService.SaveGenericAttribute(customer.Id, "Customer", Contains.SelectedPaymentMethod, null);
            }

            Update(customer);
        }

        public virtual void UpdateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            _customerRepository.Update(customer);
        }

    }
}