using App.Domain.Common;
using App.Domain.Entities.Ads;
using App.Domain.Entities.Attribute;
using App.Domain.Entities.Brandes;
using App.Domain.Entities.Data;
using App.Domain.Entities.GenericControl;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Entities.Identity;
using App.Domain.Entities.Language;
using App.Domain.Entities.Location;
using App.Domain.Entities.Menu;
using App.Domain.Entities.Orders;
using App.Domain.Entities.Payments;
using App.Domain.Entities.Slide;
using App.Domain.Orders;
using App.Domain.Shippings;
using App.FakeEntity.Ads;
using App.FakeEntity.Attribute;
using App.FakeEntity.Brandes;
using App.FakeEntity.Common;
using App.FakeEntity.ContactInformation;
using App.FakeEntity.Gallery;
using App.FakeEntity.GenericControl;
using App.FakeEntity.Language;
using App.FakeEntity.Location;
using App.FakeEntity.Manufacturers;
using App.FakeEntity.Menu;
using App.FakeEntity.News;
using App.FakeEntity.Orders;
using App.FakeEntity.Payments;
using App.FakeEntity.Post;
using App.FakeEntity.Repairs;
using App.FakeEntity.SeoGlobal;
using App.FakeEntity.ServerMail;
using App.FakeEntity.Shippings;
using App.FakeEntity.Slide;
using App.FakeEntity.Static;
using App.FakeEntity.System;
using App.FakeEntity.User;
using AutoMapper;

namespace App.Framework.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Language, LanguageFormViewModel>();
            CreateMap<ServerMailSetting, ServerMailSettingViewModel>();
            CreateMap<ContactInformation, ContactInformationViewModel>();
            CreateMap<SystemSetting, SystemSettingViewModel>();
            CreateMap<MenuLink, MenuLinkViewModel>();
            CreateMap<Province, ProvinceViewModel>();
            CreateMap<District, DistrictViewModel>();

            CreateMap<Post, PostViewModel>();
            CreateMap<PostGallery, PostGalleryViewModel>();

            CreateMap<News, NewsViewModel>();
            CreateMap<SettingSeoGlobal, SettingSeoGlobalViewModel>();
            CreateMap<PageBanner, PageBannerViewModel>();
            CreateMap<Banner, BannerViewModel>();
            CreateMap<StaticContent, StaticContentViewModel>();
            CreateMap<GalleryImage, GalleryImageViewModel>();
            CreateMap<Manufacturer, ManufacturerViewModel>();
            CreateMap<IdentityUser, RegisterFormViewModel>();
            CreateMap<AttributeValue, AttributeValueViewModel>();
            CreateMap<Attribute, AttributeViewModel>();
            CreateMap<SlideShow, SlideShowViewModel>();

            CreateMap<Brand, BrandViewModel>();
            CreateMap<Repair, RepairViewModel>();
            CreateMap<RepairGallery, RepairGalleryViewModel>();
            CreateMap<RepairItem, RepairItemViewModel>();
            CreateMap<LocalizedProperty, LocalizedPropertyViewModel>();
            CreateMap<LocaleStringResource, LocaleStringResourceViewModel>();

            CreateMap<GenericControl, GenericControlViewModel>();
            CreateMap<GenericControlValue, GenericControlValueViewModel>();
            CreateMap<GenericControlValueItem, GenericControlValueItemViewModel>();

            CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();

            CreateMap<Address, AddressViewModel>();

            CreateMap<PaymentMethod, PaymentMethodViewModel>();

            CreateMap<ShippingMethod, ShippingMethodViewModel>();

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItem, OrderItemViewModel>();
        }
    }
}