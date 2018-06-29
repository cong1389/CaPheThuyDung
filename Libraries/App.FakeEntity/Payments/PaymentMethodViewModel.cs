using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.Payments
{
    public class PaymentMethodViewModel : ILocalizedModel<PaymentMethodLocalesViewModel>
    {
        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Title", ResourceType = typeof(FormUI))]
        public string PaymentMethodSystemName
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(FormUI))]
        public string Description
        {
            get;
            set;
        }

        [Display(Name = "Image", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase Image
        {
            get;
            set;
        }

        [Display(Name = "ImageUrl", ResourceType = typeof(FormUI))]
        public string ImageUrl
        {
            get;
            set;
        }

        [Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
        public int OrderDisplay
        {
            get;
            set;
        }

        [Display(Name = "Status", ResourceType = typeof(FormUI))]
        public int Status
        {
            get;
            set;
        }

        public IList<PaymentMethodLocalesViewModel> Locales { get; set; }

        public PaymentMethodViewModel()
        {
            Locales = new List<PaymentMethodLocalesViewModel>();
        }
    }

    public class PaymentMethodLocalesViewModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        public int LocalesId { get; set; }

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Title", ResourceType = typeof(FormUI))]
        public string PaymentMethodSystemName
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(FormUI))]
        public string Description
        {
            get;
            set;
        }

        [Display(Name = "Image", ResourceType = typeof(FormUI))]
        public HttpPostedFileBase Image
        {
            get;
            set;
        }

        [Display(Name = "ImageUrl", ResourceType = typeof(FormUI))]
        public string ImageUrl
        {
            get;
            set;
        }

        [Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
        public int OrderDisplay
        {
            get;
            set;
        }

        [Display(Name = "Status", ResourceType = typeof(FormUI))]
        public int Status
        {
            get;
            set;
        }
    }
}