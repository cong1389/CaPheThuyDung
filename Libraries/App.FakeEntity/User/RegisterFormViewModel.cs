using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.User
{
    public class RegisterFormViewModel
    {
        [Display(Name = "Địa chỉ")]
        [MaxLength(200)]
        public string Address
        {
            get;
            set;
        }

        [Display(Name = "Tỉnh thành test")]
        [MaxLength(200)]
        public string City
        {
            get;
            set;
        }

        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không chính xác.")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        public DateTime? Created
        {
            get;
            set;
        }

        [DisplayName("Email")]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Email không đúng định dạng.")]
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [StringLength(150, ErrorMessage = "{0} phải có íth nhất {2} ký tự.", MinimumLength = 1)]
        public string FirstName
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        [Display(Name = "Khoá tài khoản")]
        public bool IsLockedOut
        {
            get;
            set;
        }

        [Display(Name = "Quản lý cao cấp")]
        public bool IsSuperAdmin
        {
            get;
            set;
        }

        [Display(Name = "Họ")]
        [MaxLength(150)]
        public string LastName
        {
            get;
            set;
        }

        [Display(Name = "MiddleName", ResourceType = typeof(FormUI))]
        [MaxLength(150)]
        public string MiddleName
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
        public string Password
        {
            get;
            set;
        }

        [Display(Name = "PhoneNumber", ResourceType = typeof(FormUI))]
        [MaxLength(12)]
        public string Phone
        {
            get;
            set;
        }

        [Display(Name = "Quận/Huyện")]
        [MaxLength(200)]
        public string State
        {
            get;
            set;
        }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        public string UserName
        {
            get;
            set;
        }
    }
}