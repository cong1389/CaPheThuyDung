using System;
using App.Domain.Common;
using App.Domain.Entities.Data;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Entities.Menu;
using App.Domain.Entities.Payments;
using App.Domain.Entities.Slide;
using App.Domain.Orders;
using App.FakeEntity.Common;
using App.FakeEntity.Menu;
using App.FakeEntity.Orders;
using App.FakeEntity.Payments;
using App.Service.Addresses;
using App.Service.Language;

namespace App.Front.Extensions
{
    public static class MappingExtensions
    {
        public static StaticContent ToModel(this StaticContent entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new StaticContent
            {
                Id = entity.Id,
                MenuId = entity.MenuId,
                VirtualCategoryId = entity.VirtualCategoryId,
                Language = entity.Language,
                Status = entity.Status,
                SeoUrl = entity.SeoUrl,
                ImagePath = entity.ImagePath,
                MenuLink = entity.MenuLink,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                ShortDesc = entity.GetLocalized(x => x.ShortDesc, entity.Id),
                Description = entity.GetLocalized(x => x.Description, entity.Id),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle, entity.Id),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords, entity.Id),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription, entity.Id)

            };
            return model;
        }

        public static News ToModel(this News entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new News
            {
                Id = entity.Id,
                MenuId = entity.MenuId,
                VirtualCategoryId = entity.VirtualCategoryId,
                Language = entity.Language,
                Status = entity.Status,
                SeoUrl = entity.SeoUrl,
                ImageBigSize = entity.ImageBigSize,
                ImageMediumSize = entity.ImageMediumSize,
                ImageSmallSize = entity.ImageSmallSize,
                CreatedDate = entity.CreatedDate,
                MenuLink = entity.MenuLink,
                OrderDisplay = entity.OrderDisplay,
                OtherLink = entity.OtherLink,
                SpecialDisplay = entity.SpecialDisplay,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                ShortDesc = entity.GetLocalized(x => x.ShortDesc, entity.Id),
                Description = entity.GetLocalized(x => x.Description, entity.Id),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle, entity.Id),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords, entity.Id),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription, entity.Id)
            };

            return model;
        }

        public static Post ToModel(this Post entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new Post
            {
                Id = entity.Id,
                MenuId = entity.MenuId,
                VirtualCategoryId = entity.VirtualCategoryId,
                Language = entity.Language,
                Status = entity.Status,
                SeoUrl = entity.SeoUrl,
                ImageBigSize = entity.ImageBigSize,
                ImageMediumSize = entity.ImageMediumSize,
                ImageSmallSize = entity.ImageSmallSize,
                Price = entity.Price,
                Discount = entity.Discount,
                ProductHot = entity.ProductHot,
                OutOfStock = entity.OutOfStock,
                ProductNew = entity.ProductNew,
                VirtualCatUrl = entity.VirtualCatUrl,
                StartDate = entity.StartDate,
                PostType = entity.PostType,
                OldOrNew = entity.OldOrNew,
                MenuLink = entity.MenuLink,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy,
                CreatedBy = entity.CreatedBy,

                PostGallerys = entity.PostGallerys,
                GalleryImages = entity.GalleryImages,
                AttributeValues = entity.AttributeValues,
                ManufacturerId = entity.ManufacturerId,
                Manufacturer = entity.Manufacturer,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                ProductCode = entity.GetLocalized(x => x.ProductCode, entity.Id),
                TechInfo = entity.GetLocalized(x => x.TechInfo, entity.Id),
                ShortDesc = entity.GetLocalized(x => x.ShortDesc, entity.Id),
                Description = entity.GetLocalized(x => x.Description, entity.Id),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle, entity.Id),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords, entity.Id),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription, entity.Id)

            };

            return model;
        }

        public static MenuLink ToModel(this MenuLink entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new MenuLink
            {
                Id = entity.Id,
                ParentId = entity.ParentId,
                Status = entity.Status,
                TypeMenu = entity.TypeMenu,
                Position = entity.Position,
                MenuName = entity.GetLocalized(x => x.MenuName, entity.Id),
                SeoUrl = entity.SeoUrl,
                OrderDisplay = entity.OrderDisplay,
                ImageBigSize = entity.ImageBigSize,
                ImageMediumSize = entity.ImageMediumSize,
                ImageSmallSize = entity.ImageSmallSize,
                ColorHex = entity.ColorHex,
                CurrentVirtualId = entity.CurrentVirtualId,
                VirtualId = entity.VirtualId,
                TemplateType = entity.TemplateType,
                MetaTitle = entity.GetLocalized(x => x.MetaTitle, entity.Id),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords, entity.Id),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription, entity.Id),

                Language = entity.Language,
                SourceLink = entity.SourceLink,
                VirtualSeoUrl = entity.VirtualSeoUrl,
                DisplayOnHomePage = entity.DisplayOnHomePage,
                DisplayOnMenu = entity.DisplayOnMenu,
                DisplayOnSearch = entity.DisplayOnSearch
            };

            return model;
        }

        public static MenuNavViewModel ToModel(this MenuLink entity, MenuNavViewModel destination)
        {
            if (entity == null)
            {
                return null;
            }

            destination.MenuId = entity.Id;
            destination.ParentId = entity.ParentId;
            destination.MenuName = entity.GetLocalized(x => x.MenuName, entity.Id);
            destination.SeoUrl = entity.SeoUrl;
            destination.OrderDisplay = entity.OrderDisplay;
            destination.ImageBigSize = entity.ImageBigSize;
            destination.CurrentVirtualId = entity.CurrentVirtualId;
            destination.VirtualId = entity.VirtualId;
            destination.TemplateType = entity.TemplateType;
            destination.ImageMediumSize = entity.ImageMediumSize;
            destination.ImageSmallSize = entity.ImageSmallSize;

            return destination;
        }

        public static ContactInformation ToModel(this ContactInformation entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new ContactInformation
            {
                Id = entity.Id,
                Email = entity.Email,
                Fax = entity.Fax,
                Hotline = entity.Hotline,
                Lag = entity.Lag,
                Language = entity.Language,
                Lat = entity.Lat,
                MobilePhone = entity.MobilePhone,
                NumberOfStore = entity.NumberOfStore,
                OrderDisplay = entity.OrderDisplay,
                ProvinceId = entity.ProvinceId,
                Status = entity.Status,
                Type = entity.Type,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                Address = entity.GetLocalized(x => x.Address, entity.Id)
            };
            return model;
        }

        public static SystemSetting ToModel(this SystemSetting entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new SystemSetting
            {
                Id = entity.Id,
                Language = entity.Language,
                Status = entity.Status,
                FaviconImage = entity.FaviconImage,
                LogoImage = entity.LogoImage,
                LogoFooterImage = entity.LogoFooterImage,
                MaintanceSite = entity.MaintanceSite,
                Hotline = entity.Hotline,
                Email = entity.Email,
                TimeWork = entity.TimeWork,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                FooterContent = entity.GetLocalized(x => x.FooterContent, entity.Id),
                MetaTitle = entity.GetLocalized(x => x.MetaTitle, entity.Id),
                MetaDescription = entity.GetLocalized(x => x.MetaDescription, entity.Id),
                MetaKeywords = entity.GetLocalized(x => x.MetaKeywords, entity.Id),
                Description = entity.GetLocalized(x => x.Description, entity.Id)
            };

            return model;
        }

        public static Address ToEntity(this AddressViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            var entity = new Address();
            return ToEntity(model, entity);
        }

        public static Address ToEntity(this AddressViewModel model, Address destination)
        {
            if (model == null)
            {
                return destination;
            }

            destination.Id = model.Id;
            destination.Salutation = model.Salutation;
            destination.Title = model.Title;
            destination.FirstName = model.FirstName;
            destination.LastName = model.LastName;
            destination.Email = model.Email;
            destination.Company = model.Company;
            destination.CountryId = model.CountryId;
            destination.StateProvinceId = model.StateProvinceId;
            destination.City = model.City;
            destination.Address1 = model.Address1;
            destination.Address2 = model.Address2;
            destination.ZipPostalCode = model.ZipPostalCode;
            destination.PhoneNumber = model.PhoneNumber;
            destination.FaxNumber = model.FaxNumber;

            return destination;
        }

        public static void PrepareModel(this Address model, Address destination)
        {
            model.Id = destination.Id;
            model.Salutation = destination.Salutation;
            model.Title = destination.Title;
            model.FirstName = destination.FirstName;
            model.LastName = destination.LastName;
            model.Email = destination.Email;
            model.Company = destination.Company;
            model.CountryId = destination.CountryId;
            model.StateProvinceId = destination.StateProvinceId;
            model.City = destination.City;
            model.Address1 = destination.Address1;
            model.Address2 = destination.Address2;
            model.ZipPostalCode = destination.ZipPostalCode;
            model.PhoneNumber = destination.PhoneNumber;
            model.FaxNumber = destination.FaxNumber;
        }

        public static PaymentMethodViewModel ToModel(this PaymentMethod model)
        {
            if (model == null)
            {
                return null;
            }

            var pamentMethod = new PaymentMethodViewModel
            {
                ImageUrl = model.ImageUrl,
                Status = model.Status,
                PaymentMethodSystemName = model.GetLocalized(x => x.PaymentMethodSystemName, model.Id),
                Description = model.GetLocalized(x => x.Description, model.Id),
            };

            return pamentMethod;
        }

        public static OrderViewModel ToModel(this Order model, OrderViewModel destination)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Order");
            }

            if (destination == null)
            {
                throw new ArgumentNullException("OrderViewModel");
            }

            destination.Id = model.Id;
            destination.OrderNumber = model.OrderNumber;

            destination.OrderGuid = model.OrderGuid;
            destination.StoreId = model.StoreId;
            destination.CustomerId = model.CustomerId;
            destination.BillingAddressId = model.BillingAddressId;
            destination.ShippingAddressId = model.ShippingAddressId;
            destination.OrderStatusId = model.OrderStatusId;
            destination.ShippingStatusId = model.ShippingStatusId;
            destination.PaymentStatusId = model.PaymentStatusId;
            destination.PaymentMethodSystemName = model.PaymentMethodSystemName;
            destination.CustomerCurrencyCode = model.CustomerCurrencyCode;
            destination.CurrencyRate = model.CurrencyRate;

            destination.CustomerTaxDisplayTypeId = model.CustomerTaxDisplayTypeId;
            destination.VatNumber = model.VatNumber;
            destination.OrderSubtotalInclTax = model.OrderSubtotalInclTax;
            destination.OrderSubtotalExclTax = model.OrderSubtotalExclTax;

            destination.OrderSubTotalDiscountInclTax = model.OrderSubTotalDiscountInclTax;
            destination.OrderSubTotalDiscountExclTax = model.OrderSubTotalDiscountExclTax;
            destination.OrderShippingInclTax = model.OrderShippingInclTax;
            destination.OrderShippingExclTax = model.OrderShippingExclTax;

            destination.PaymentMethodAdditionalFeeInclTax = model.PaymentMethodAdditionalFeeInclTax;
            destination.PaymentMethodAdditionalFeeExclTax = model.PaymentMethodAdditionalFeeExclTax;
            destination.TaxRates = model.TaxRates;
            destination.OrderTax = model.OrderTax;

            destination.OrderDiscount = model.OrderDiscount;
            destination.OrderTotal = model.OrderTotal;
            destination.RefundedAmount = model.RefundedAmount;
            destination.RewardPointsWereAdded = model.RewardPointsWereAdded;

            destination.CheckoutAttributeDescription = model.CheckoutAttributeDescription;
            destination.CheckoutAttributesXml = model.CheckoutAttributesXml;
            destination.CustomerLanguageId = model.CustomerLanguageId;
            destination.AffiliateId = model.AffiliateId;

            destination.CustomerIp = model.CustomerIp;
            destination.AllowStoringCreditCardNumber = model.AllowStoringCreditCardNumber;
            destination.CardType = model.CardType;
            destination.CardName = model.CardName;

            destination.CardNumber = model.CardNumber;
            destination.MaskedCreditCardNumber = model.MaskedCreditCardNumber;
            destination.CardCvv2 = model.CardCvv2;
            destination.CardExpirationMonth = model.CardExpirationMonth;

            destination.CardExpirationYear = model.CardExpirationYear;
            destination.AllowStoringDirectDebit = model.AllowStoringDirectDebit;
            destination.DirectDebitAccountHolder = model.DirectDebitAccountHolder;
            destination.DirectDebitAccountNumber = model.DirectDebitAccountNumber;

            destination.DirectDebitBankCode = model.DirectDebitBankCode;
            destination.DirectDebitBankName = model.DirectDebitBankName;
            destination.DirectDebitBIC = model.DirectDebitBIC;
            destination.DirectDebitCountry = model.DirectDebitCountry;

            destination.DirectDebitIban = model.DirectDebitIban;
            destination.AuthorizationTransactionId = model.AuthorizationTransactionId;
            destination.AuthorizationTransactionCode = model.AuthorizationTransactionCode;
            destination.AuthorizationTransactionResult = model.AuthorizationTransactionResult;

            destination.CaptureTransactionId = model.CaptureTransactionId;
            destination.CaptureTransactionResult = model.CaptureTransactionResult;
            destination.SubscriptionTransactionId = model.SubscriptionTransactionId;
            destination.PurchaseOrderNumber = model.PurchaseOrderNumber;

            destination.PaidDateUtc = model.PaidDateUtc;
            destination.ShippingMethod = model.ShippingMethod;
            destination.ShippingRateComputationMethodSystemName = model.ShippingRateComputationMethodSystemName;
            destination.Deleted = model.Deleted;

            destination.CreatedOnUtc = model.CreatedOnUtc;
            destination.UpdatedOnUtc = model.UpdatedOnUtc;
            destination.RewardPointsRemaining = model.RewardPointsRemaining;
            destination.CustomerOrderComment = model.CustomerOrderComment;

            destination.OrderShippingTaxRate = model.OrderShippingTaxRate;
            destination.PaymentMethodAdditionalFeeTaxRate = model.PaymentMethodAdditionalFeeTaxRate;
            destination.HasNewPaymentNotification = model.HasNewPaymentNotification;
            destination.AcceptThirdPartyEmailHandOver = model.AcceptThirdPartyEmailHandOver;

            destination.CreatedOn = model.CreatedOnUtc;
            destination.UpdatedOn = model.UpdatedOnUtc;
            destination.OrderTotal = model.OrderTotal;

            destination.OrderStatus = ((OrderStatus)model.OrderStatusId).ToString();
            destination.PaymentStatus = ((PaymentStatus)model.PaymentStatusId).ToString();
            destination.ShippingStatus = ((ShippingStatus)model.ShippingStatusId).ToString();

            destination.BillingAddress = model.BillingAddress;
            destination.ShippingAddress = model.ShippingAddress;
            destination.CustomerName = model.BillingAddress.GetFullName();
            destination.CustomerEmail = model.BillingAddress.Email;

            return destination;
        }

        public static MenuLinkViewModel ToModel(this MenuLink model, MenuLinkViewModel destination)
        {
            if (model == null)
            {
                throw new ArgumentNullException("MenuLink");
            }

            if (destination == null)
            {
                throw new ArgumentNullException("MenuLinkViewModel");
            }

            destination.Id = model.Id;
            destination.ParentId = model.ParentId;
            destination.Status = model.Status;
            destination.TypeMenu = model.TypeMenu;
            destination.Position = model.Position;
            destination.MenuName = model.MenuName;
            destination.SeoUrl = model.SeoUrl;
            destination.OrderDisplay = model.OrderDisplay;
            destination.ImageBigSize = model.ImageBigSize;
            destination.ImageMediumSize = model.ImageMediumSize;
            destination.ImageSmallSize = model.ImageSmallSize;
            destination.CurrentVirtualId = model.CurrentVirtualId;
            destination.VirtualId = model.VirtualId;
            destination.TemplateType = model.TemplateType;
            destination.MetaTitle = model.GetLocalized(x => x.MetaTitle, model.Id);
            destination.MetaKeywords = model.GetLocalized(x => x.MetaKeywords, model.Id);
            destination.MetaDescription = model.GetLocalized(x => x.MetaDescription, model.Id);

            destination.Language = model.Language;
            destination.SourceLink = model.SourceLink;
            destination.VirtualSeoUrl = model.VirtualSeoUrl;
            destination.DisplayOnHomePage = model.DisplayOnHomePage;
            destination.DisplayOnMenu = model.DisplayOnMenu;
            destination.DisplayOnSearch = model.DisplayOnSearch;
            destination.GenericControls = model.GenericControls;

            return destination;
        }

        public static Manufacturer ToModel(this Manufacturer entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new Manufacturer
            {
                Id = entity.Id,
                OtherLink = entity.OtherLink,
                Status = entity.Status,
                OrderDisplay = entity.OrderDisplay,
                ImageUrl = entity.ImageUrl,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                Description = entity.GetLocalized(x => x.Description, entity.Id)

            };
            return model;
        }

        public static SlideShow ToModel(this SlideShow entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new SlideShow
            {
                Id = entity.Id,
                Status = entity.Status,
                WebsiteLink = entity.WebsiteLink,
                ImgPath = entity.ImgPath,
                Video = entity.Video,
                Width = entity.Width,
                Height = entity.Height,
                Target = entity.Target,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                OrderDisplay = entity.OrderDisplay,

                Title = entity.GetLocalized(x => x.Title, entity.Id),
                Description = entity.GetLocalized(x => x.Description, entity.Id)
            };

            return model;
        }

    }
}