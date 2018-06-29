using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Domain.Entities.Brandes;

namespace App.FakeEntity.Repairs
{
    public class RepairViewModel
    {
        [Display(Name = "Phụ kiện")]
        public string Accessories
        {
            get;
            set;
        }

        [Display(Name = "Địa chỉ khách")]
        public string Address
        {
            get;
            set;
        }

        [Display(Name = "Apple id")]
        public string AppleId
        {
            get;
            set;
        }

        public virtual Brand Brand
        {
            get;
            set;
        }

        [Display(Name = "Thương hiệu")]
        public int BrandId
        {
            get;
            set;
        }

        [Display(Name = "Hạng mục")]
        public string Category
        {
            get;
            set;
        }

        [Display(Name = "Mã khách")]
        public string CustomerCode
        {
            get;
            set;
        }

        [Display(Name = "CMND")]
        public string CustomerIdNumber
        {
            get;
            set;
        }

        [Display(Name = "Họ tên khách")]
        public string CustomerName
        {
            get;
            set;
        }

        [Display(Name = "Chi phí")]
        public decimal? FixedFee
        {
            get;
            set;
        }

        [Display(Name = "Danh mục sửa chữa")]
        public string FixedTags
        {
            get;
            set;
        }

        [Display(Name = "Mật khẩu icloud")]
        public string IcloudPassword
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Hình ảnh")]
        public string ImagePhone
        {
            get;
            set;
        }

        public string Model
        {
            get;
            set;
        }

        [Display(Name = "Dòng máy")]
        public string ModelBrand
        {
            get;
            set;
        }

        [Display(Name = "Ghi chú")]
        public string Note
        {
            get;
            set;
        }

        [Display(Name = "Bảo hành cũ")]
        public string OldWarranty
        {
            get;
            set;
        }

        [Display(Name = "Mã hoá đơn")]
        public string RepairCode
        {
            get;
            set;
        }

        public virtual ICollection<RepairGalleryViewModel> RepairGalleries
        {
            get;
            set;
        }

        public virtual ICollection<RepairItemViewModel> RepairItems
        {
            get;
            set;
        }

        [Display(Name = "Mật khẩu máy")]
        public string PasswordPhone
        {
            get;
            set;
        }

        [Display(Name = "Điện thoại")]
        public string PhoneNumber
        {
            get;
            set;
        }

        [Display(Name = "Tình trạng")]
        public string PhoneStatus
        {
            get;
            set;
        }

        [Display(Name = "Imei - seri")]
        public string SerialNumber
        {
            get;
            set;
        }

        [Display(Name = "Tình trạng hiện tại")]
        public int Status
        {
            get;
            set;
        }

        [Display(Name = "Tại cửa hàng")]
        public string StoreName
        {
            get;
            set;
        }

        public DateTime? WarrantyFrom
        {
            get;
            set;
        }

        public DateTime? WarrantyTo
        {
            get;
            set;
        }

        public RepairViewModel()
        {
            RepairItems = new List<RepairItemViewModel>();
        }
    }
}