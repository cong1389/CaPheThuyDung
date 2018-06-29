using App.Core.Common;
using App.Domain.Common;
using App.Domain.Entities.Orders;
using App.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Entities.Customers
{
    public class Customer : AuditableEntity<int>
    {
        

        public Guid CustomerGuid
        {
            get; set;
        }
        public string Username
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public int PasswordFormatId
        {
            get; set;
        }
        public string PasswordSalt
        {
            get; set;
        }
        public string AdminComment
        {
            get; set;
        }
        public bool IsTaxExempt
        {
            get; set;
        }
        public int AffiliateId
        {
            get; set;
        }
        public bool Active
        {
            get; set;
        }
        public bool Deleted
        {
            get; set;
        }
        public bool IsSystemAccount
        {
            get; set;
        }
        public string SystemName
        {
            get; set;
        }
        public string LastIpAddress
        {
            get; set;
        }
        public DateTime CreatedOnUtc
        {
            get; set;
        }
        public DateTime LastLoginDateUtc
        {
            get; set;
        }
        public DateTime LastActivityDateUtc
        {
            get; set;
        }
        //public int BillingAddress_Id
        //{
        //    get; set;
        //}
        //public int ShippingAddress_Id
        //{
        //    get; set;
        //}

        private ICollection<ShoppingCartItem> _shoppingCartItems;
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get => _shoppingCartItems ?? (_shoppingCartItems = new HashSet<ShoppingCartItem>());
            protected set => _shoppingCartItems = value;
        }

        //public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ICollection<Address> _addresses;
        public virtual ICollection<Address> Addresses
        {
            get => _addresses ?? (_addresses = new HashSet<Address>());
            protected set => _addresses = value;
        }
        [DataMember]
        public virtual Address BillingAddress { get; set; }
        [DataMember]
        public virtual Address ShippingAddress { get; set; }

        //private Address _billingAddress;
        //[DataMember]
        //public virtual Address BillingAddress
        //{
        //    get { return _billingAddress ?? (_billingAddress = new Address()); }
        //    set { _billingAddress = value; }
        //}

        //private Address _shippingAddress;
        //[DataMember]
        //public virtual Address ShippingAddress
        //{
        //    get { return _shippingAddress ?? (_shippingAddress = new Address()); }
        //     set { _shippingAddress = value; }
        //}

        private ICollection<Order> _orders;
        [DataMember]
        public virtual ICollection<Order> Orders
        {
            get => _orders ?? (_orders = new HashSet<Order>());
            set => _orders = value;
        }

        #region Addresses

        public virtual void RemoveAddress(Address address)
        {
            if (!Addresses.Contains(address)) return;

            if (BillingAddress == address) BillingAddress = null;

            if (ShippingAddress == address) ShippingAddress = null;

            Addresses.Remove(address);
        }

        #endregion
    }
}