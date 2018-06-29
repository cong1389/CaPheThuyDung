using System.Configuration;
using System.IO;
using System.Web;

namespace App.Aplication
{
    public static class Contains
    {
        public static string AdsFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["AdsFolder"] ?? "images/Ads/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["AdsFolder"] ?? "images/Ads/";
            }
        }

        public static string SlideShowFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["SlideShowFolder"] ?? "images/SlideShow/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["SlideShowFolder"] ?? "images/SlideShow/";
            }
        }

        public static bool EnableSendEmai => bool.Parse(ConfigurationManager.AppSettings["EnableSendEmail"] ?? "false");

        public static bool EnableSendSms => bool.Parse(ConfigurationManager.AppSettings["EnableSendSMS"] ?? "false");

        public static string FolderLanguage
        {
            get
            {
                string item = ConfigurationManager.AppSettings["LanguageFolder"] ?? "images/language/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["LanguageFolder"] ?? "images/language/";
            }
        }

        public static string ImageNoExsits => "/Areas/Admin/images/no_image.jpg";

        public static string ImageFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["ImageFolder"] ?? "images/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["ImageFolder"] ?? "images/";
            }
        }

        public static string NewsFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["NewsFolder"] ?? "images/news/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["NewsFolder"] ?? "images/news/";
            }
        }

        public static string PostFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["PostFolder"] ?? "images/post/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["PostFolder"] ?? "images/post/";
            }
        }

        public static string StaticContentFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["StaticContentFolder"] ?? "images/staticcontent/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["StaticContentFolder"] ?? "images/staticcontent/";
            }
        }

        public static string ManufacturerFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["ManufacturerFolder"] ?? "images/Manufacturer/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["ManufacturerFolder"] ?? "images/Manufacturer/";
            }
        }

        public static string ManufactureFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["ManufactureFolder"] ?? "images/manufacture/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["ManufactureFolder"] ?? "images/manufacture/";
            }
        }

        public static string PaymentMethodFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["PaymentMethodFolder"] ?? "images/paymentmethod/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["PaymentMethodFolder"] ?? "images/paymentmethod/";
            }
        }

        public static string SystemSettingFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["SystemSettingFolder"] ?? "images/systemsetting/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["SystemSettingFolder"] ?? "images/systemsetting/";
            }
        }

        public static bool RequiredActiveAccount => bool.Parse(ConfigurationManager.AppSettings["RequiredActiveAccount"] ?? "false");

        public static string MenuFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["MenuFolder"] ?? "images/menu/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["MenuFolder"] ?? "images/menu/";
            }
        }

        public static string RepairFolder
        {
            get
            {
                string item = ConfigurationManager.AppSettings["RepairFolder"] ?? "images/repair/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(string.Concat("~/", item))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(string.Concat("~/", item)));
                }
                return ConfigurationManager.AppSettings["RepairFolder"] ?? "images/repair/";
            }
        }

        //SystemCustomerAttributeNames
        public static string SelectedPaymentMethod => "SelectedPaymentMethod";
        public static string SelectedShippingOption => "SelectedShippingOption";

        //Template mail
        public static string TemplateMailBasicContact => "Themes/Basic/TemplateMails/Contact.xml";
	    public static string TemplateMailBasicTest => "Themes/Basic/TemplateMails/Test.xml";

		//Account
		public static string XsrfKey => ConfigurationManager.AppSettings["XsrfKey"] ?? "XsrfKey";

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        
        public enum GenericControlType
        {
            /// <summary>
            /// Dropdown list
            /// </summary>
            DropdownList = 1,
            /// <summary>
            /// Radio list
            /// </summary>
            RadioList = 2,
            /// <summary>
            /// Checkboxes
            /// </summary>
            Checkboxes = 3,
            /// <summary>
            /// TextBox
            /// </summary>
            TextBox = 4,
            /// <summary>
            /// Multiline textbox
            /// </summary>
            MultilineTextbox = 10,
            /// <summary>
            /// Datepicker
            /// </summary>
            Datepicker = 20,
            /// <summary>
            /// File upload control
            /// </summary>
            FileUpload = 30,
            /// <summary>
            /// Boxes
            /// </summary>
            Boxes = 40
        }

        public enum PaymentMethodType
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// All payment information is entered on the site
            /// </summary>
            Standard = 10,
            /// <summary>
            /// A customer is redirected to a third-party site in order to complete the payment
            /// </summary>
            Redirection = 15,
            /// <summary>
            /// Button
            /// </summary>
            Button = 20,
            /// <summary>
            /// All payment information is entered on the site and is available via button
            /// </summary>
            StandardAndButton = 25,
            /// <summary>
            /// Payment information is entered in checkout and customer is redirected to complete payment (e.g. 3D Secure) after order has been placed
            /// </summary>
            StandardAndRedirection = 30
        }
    }

   

   

}