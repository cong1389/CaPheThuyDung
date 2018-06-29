using System;
using System.Collections.Generic;
using App.Domain.Entities.Orders;

namespace FakeEntity.Customers
{
	public class CutomerViewModel
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
        public int BillingAddress_Id
        {
            get; set;
        }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get;
            set;
        }

        public int ShippingAddress_Id
        {
            get; set;
        }
    }
}