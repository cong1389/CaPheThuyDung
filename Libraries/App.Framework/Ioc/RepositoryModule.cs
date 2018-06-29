using App.Core.Caching;
using App.Domain.Interfaces.Repository;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.Account;
using App.Infra.Data.Repository.Addresses;
using App.Infra.Data.Repository.Ads;
using App.Infra.Data.Repository.Attribute;
using App.Infra.Data.Repository.Brandes;
using App.Infra.Data.Repository.ContactInformation;
using App.Infra.Data.Repository.Customers;
using App.Infra.Data.Repository.Gallery;
using App.Infra.Data.Repository.GenericAttribute;
using App.Infra.Data.Repository.GenericControl;
using App.Infra.Data.Repository.Language;
using App.Infra.Data.Repository.LocaleStringResource;
using App.Infra.Data.Repository.Locations;
using App.Infra.Data.Repository.MailSetting;
using App.Infra.Data.Repository.Manufacturers;
using App.Infra.Data.Repository.Menu;
using App.Infra.Data.Repository.News;
using App.Infra.Data.Repository.Orderes;
using App.Infra.Data.Repository.Orders;
using App.Infra.Data.Repository.Other;
using App.Infra.Data.Repository.PaymentMethodes;
using App.Infra.Data.Repository.Post;
using App.Infra.Data.Repository.Repairs;
using App.Infra.Data.Repository.SeoSetting;
using App.Infra.Data.Repository.Settings;
using App.Infra.Data.Repository.ShippingMethods;
using App.Infra.Data.Repository.Slide;
using App.Infra.Data.Repository.Static;
using App.Infra.Data.Repository.System;
using App.Infra.Data.RepositoryAsync.Post;
using Autofac;

namespace App.Framework.Ioc
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>));
            builder.RegisterGeneric(typeof(RepositoryBaseAsync<>)).As(typeof(IRepositoryBaseAsync<>));
            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>().InstancePerRequest();
            builder.RegisterType<MailSettingRepository>().As<IMailSettingRepository>().InstancePerRequest();
            builder.RegisterType<ContactInfoRepository>().As<IContactInfoRepository>().InstancePerRequest();
            builder.RegisterType<SystemSettingRepository>().As<ISystemSettingRepository>().InstancePerRequest();
            builder.RegisterType<MenuLinkRepository>().As<IMenuLinkRepository>().InstancePerRequest();
            builder.RegisterType<ProvinceRepository>().As<IProvinceRepository>().InstancePerRequest();
            builder.RegisterType<DistrictRepository>().As<IDistrictRepository>().InstancePerRequest();

            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerRequest();
            builder.RegisterType<PostGalleryRepository>().As<IPostGalleryRepository>().InstancePerRequest();

            builder.RegisterType<NewsRepository>().As<INewsRepository>().InstancePerRequest();
            builder.RegisterType<StaticContentRepository>().As<IStaticContentRepository>().InstancePerRequest();
            builder.RegisterType<SettingSeoGlobalRepository>().As<ISettingSeoGlobalRepository>().InstancePerRequest();
            builder.RegisterType<PageBannerRepository>().As<IPageBannerRepository>().InstancePerRequest();
            builder.RegisterType<LandingPageRepository>().As<ILandingPageRepository>().InstancePerRequest();
            builder.RegisterType<BannerRepository>().As<IBannerRepository>().InstancePerRequest();
            builder.RegisterType<GalleryRepository>().As<IGalleryRepository>().InstancePerRequest();
            builder.RegisterType<AttributeRepository>().As<IAttributeRepository>().InstancePerRequest();
            builder.RegisterType<AttributeValueRepository>().As<IAttributeValueRepository>().InstancePerRequest();
            builder.RegisterType<SlideShowRepository>().As<ISlideShowRepository>().InstancePerRequest();
            builder.RegisterType<ManufacturerRepository>().As<IManufacturerRepository>().InstancePerRequest();
            builder.RegisterType<PostRepositoryAsync>().As<IPostRepositoryAsync>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
            builder.RegisterType<ExternalLoginRepository>().As<IExternalLoginRepository>().InstancePerRequest();
         
            builder.RegisterType<BrandRepository>().As<IBrandRepository>().InstancePerRequest();
            builder.RegisterType<RepairRepository>().As<IRepairRepository>().InstancePerRequest();
            builder.RegisterType<RepairGalleryRepository>().As<IRepairGalleryRepository>().InstancePerRequest();
            builder.RegisterType<RepairItemRepository>().As<IRepairItemRepository>().InstancePerRequest();
            builder.RegisterType<LocalizedPropertyRepository>().As<ILocalizedPropertyRepository>().InstancePerRequest();
            builder.RegisterType<GenericAttributeRepository>().As<IGenericAttributeRepository>().InstancePerRequest();
            builder.RegisterType<LocaleStringResourceRepository>().As<ILocaleStringResourceRepository>().InstancePerRequest();

            builder.RegisterType<GenericControlRepository>().As<IGenericControlRepository>().InstancePerRequest();
            builder.RegisterType<GenericControlValueRepository>().As<IGenericControlValueRepository>().InstancePerRequest();
            builder.RegisterType<GenericControlValueItemRepository>().As<IGenericControlValueItemRepository>().InstancePerRequest();

            builder.RegisterType<ShoppingCartItemRepository>().As<IShoppingCartItemRepository>().InstancePerRequest();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerRequest();

            builder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerRequest();

            builder.RegisterType<PaymentMethodRepository>().As<IPaymentMethodRepository>().InstancePerRequest();

            builder.RegisterType<ShippingMethodRepository>().As<IShippingMethodRepository>().InstancePerRequest();

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<MemoryCacheManager>().Named<ICacheManager>("memory").SingleInstance();

            // Request cache
            //builder.RegisterType<RequestCache>().As<IRequestCache>().InstancePerRequest();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerRequest();
            builder.RegisterType<OrderItemRepository>().As<IOrderItemRepository>().InstancePerRequest();

	        builder.RegisterType<SettingRepository>().As<ISettingRepository>().InstancePerRequest();

		}
    }

}