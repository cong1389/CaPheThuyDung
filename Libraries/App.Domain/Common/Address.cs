using App.Core.Common;

namespace App.Domain.Common
{
    public class Address: AuditableEntity<int>
    {       
        public string FirstName
        {
            get;set;            
        }
        public string LastName
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string Company
        {
            get; set;
        }
        public int? CountryId
        {
            get; set;
        }
        public int? StateProvinceId
        {
            get; set;
        }
        public string City
        {
            get; set;
        }
        public string Address1
        {
            get; set;
        }
        public string Address2
        {
            get; set;
        }
        public string ZipPostalCode
        {
            get; set;
        }
        public string PhoneNumber
        {
            get; set;
        }
        public string FaxNumber
        {
            get; set;
        }
     
        public string Salutation
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }

        //public virtual ICollection<Customer> Customers { get; set; }


    }
}
