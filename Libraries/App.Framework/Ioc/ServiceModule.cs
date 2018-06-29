using App.AsyncService.Post;
using App.Core;
using App.Core.IO.VirtualPath;
using App.Core.Plugins;
using App.Core.Plugins.Providers;
using App.Core.Templating;
using App.Domain.Interfaces.Services;
using App.Framework.Plugins;
using App.Framework.Templating.Liquid;
using App.Infra.Data.Common;
using App.SeoSitemap;
using App.Service.Account;
using App.Service.Addresses;
using App.Service.Ads;
using App.Service.Attribute;
using App.Service.Brandes;
using App.Service.Common;
using App.Service.ContactInformation;
using App.Service.Customers;
using App.Service.Gallery;
using App.Service.GenericAttribute;
using App.Service.GenericControl;
using App.Service.Language;
using App.Service.LocaleStringResource;
using App.Service.LocalizedProperty;
using App.Service.Locations;
using App.Service.MailSetting;
using App.Service.Manufacturers;
using App.Service.Media;
using App.Service.Menu;
using App.Service.Messages;
using App.Service.News;
using App.Service.Orders;
using App.Service.Other;
using App.Service.PaymentMethodes;
using App.Service.Post;
using App.Service.Repair;
using App.Service.Repairs;
using App.Service.SeoSetting;
using App.Service.Settings;
using App.Service.ShippingMethodes;
using App.Service.Slide;
using App.Service.Static;
using App.Service.SystemApp;
using Autofac;

namespace App.Framework.Ioc
{
    public class ServiceModule : Module
    {
        /// <summary>
        /// Đăng ký AutoFac
        /// </summary>
        /// <param name="builder"></param>
		protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>));
            builder.RegisterGeneric(typeof(BaseAsyncService<>)).As(typeof(IBaseAsyncService<>));

            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerRequest();
            builder.RegisterType<MailSettingService>().As<IMailSettingService>().InstancePerRequest();
            builder.RegisterType<ContactInfoService>().As<IContactInfoService>().InstancePerRequest();
            builder.RegisterType<SystemSettingService>().As<ISystemSettingService>().InstancePerRequest();
            builder.RegisterType<MenuLinkService>().As<IMenuLinkService>().InstancePerRequest();
            builder.RegisterType<ProvinceService>().As<IProvinceService>().InstancePerRequest();
            builder.RegisterType<DistrictService>().As<IDistrictService>().InstancePerRequest();

            builder.RegisterType<PostService>().As<IPostService>().InstancePerRequest();
            builder.RegisterType<PostGalleryService>().As<IPostGalleryService>().InstancePerRequest();

            builder.RegisterType<NewsService>().As<INewsService>().InstancePerRequest();
            builder.RegisterType<StaticContentService>().As<IStaticContentService>().InstancePerRequest();
            builder.RegisterType<SettingSeoGlobalService>().As<ISettingSeoGlobalService>().InstancePerRequest();

            builder.RegisterType<PageBannerService>().As<IPageBannerService>().InstancePerRequest();
            builder.RegisterType<BannerService>().As<IBannerService>().InstancePerRequest();

            builder.RegisterType<GalleryService>().As<IGalleryService>().InstancePerRequest();

            builder.RegisterType<AttributeService>().As<IAttributeService>().InstancePerRequest();
            builder.RegisterType<AttributeValueService>().As<IAttributeValueService>().InstancePerRequest();

            builder.RegisterType<SlideShowService>().As<ISlideShowService>().InstancePerRequest();
            builder.RegisterType<LandingPageService>().As<ILandingPageService>().InstancePerRequest();
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>().InstancePerRequest();
            builder.RegisterType<SitemapProvider>().As<ISitemapProvider>().InstancePerRequest();

            builder.RegisterType<PostAsynService>().As<IPostAsynService>().InstancePerRequest();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerRequest();
            builder.RegisterType<UserStoreService>().As<IUserStoreService>().InstancePerRequest();
            builder.RegisterType<SendMailService>().As<ISendMailService>().InstancePerRequest();
           
            builder.RegisterType<BrandService>().As<IBrandService>().InstancePerRequest();

            builder.RegisterType<RepairService>().As<IRepairService>().InstancePerRequest();
            builder.RegisterType<RepairGalleryService>().As<IRepairGalleryService>().InstancePerRequest();
            builder.RegisterType<RepairItemService>().As<IRepairItemService>().InstancePerRequest();

            builder.RegisterType<LocalizedPropertyService>().As<ILocalizedPropertyService>().InstancePerRequest();
            builder.RegisterType<CommonServices>().As<ICommonServices>().InstancePerRequest();
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerRequest();
            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerRequest();
            builder.RegisterType<LocaleStringResourceService>().As<ILocaleStringResourceService>().InstancePerRequest();
            builder.RegisterType<TextService>().As<ITextService>().InstancePerRequest();

            builder.RegisterType<GenericControlService>().As<IGenericControlService>().InstancePerRequest();
            builder.RegisterType<GenericControlValueService>().As<IGenericControlValueService>().InstancePerRequest();
            builder.RegisterType<GenericControlValueItemService>().As<IGenericControlValueItemService>().InstancePerRequest();

            builder.RegisterType<ShoppingCartItemService>().As<IShoppingCartItemService>().InstancePerRequest();

            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerRequest();

            builder.RegisterType<OrderTotalCalculationService>().As<IOrderTotalCalculationService>().InstancePerRequest();

            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerRequest();

            builder.RegisterType<PaymentMethodService>().As<IPaymentMethodService>().InstancePerRequest();

            builder.RegisterType<ShippingMethodService>().As<IShippingMethodService>().InstancePerRequest();

            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>().InstancePerRequest();

            builder.RegisterType<OrderProcessingService>().As<IOrderProcessingService>().InstancePerRequest();
            builder.RegisterType<PriceCalculationService>().As<IPriceCalculationService>().InstancePerRequest();

	        builder.RegisterType<MessageService>().As<IMessageService>().InstancePerRequest();

	        builder.RegisterType<LiquidTemplateEngine>().As<ITemplateEngine>().InstancePerRequest();
            builder.RegisterType<DefaultVirtualPathProvider>().As<IVirtualPathProvider>().InstancePerRequest();
            builder.RegisterType<MessageModelProvider>().As<IMessageModelProvider>().InstancePerRequest();
            builder.RegisterType<DefaultTemplateManager>().As<ITemplateManager>().InstancePerRequest();

	        builder.RegisterType<ImageService>().As<IImageService>().InstancePerRequest();
	        builder.RegisterType<SettingService>().As<ISettingService>().InstancePerRequest();

            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerRequest();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerRequest();
            builder.RegisterType<ProviderManager>().As<IProviderManager>().InstancePerRequest();

        }
    }
}