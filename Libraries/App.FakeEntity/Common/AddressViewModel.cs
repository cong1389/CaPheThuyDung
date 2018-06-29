using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.Common
{
    public class AddressViewModel
    {
        public int Id
        {
            get;
            set;
        }

        [Display(Name = "FirstName", ResourceType = typeof(FormUI))]
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [StringLength(150, ErrorMessage = "{0} phải có íth nhất {2} ký tự.", MinimumLength = 1)]
        public string FirstName
        {
            get; set;
        }

        [Display(Name = "LastName", ResourceType = typeof(FormUI))]
        [MaxLength(150)]
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
        public int CountryId
        {
            get; set;
        }
        public int StateProvinceId
        {
            get; set;
        }
        public string City
        {
            get; set;
        }

        [Display(Name = "Address1", ResourceType = typeof(FormUI))]
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

        [Display(Name = "PhoneNumber", ResourceType = typeof(FormUI))]
        [MaxLength(12)]
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
    }
}
