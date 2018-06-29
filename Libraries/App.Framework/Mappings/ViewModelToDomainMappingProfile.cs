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
using App.Domain.Entities.Other;
using App.Domain.Entities.Payments;
using App.Domain.Entities.Setting;
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
using App.FakeEntity.Other;
using App.FakeEntity.Payments;
using App.FakeEntity.Post;
using App.FakeEntity.Repairs;
using App.FakeEntity.SeoGlobal;
using App.FakeEntity.ServerMail;
using App.FakeEntity.Setting;
using App.FakeEntity.Shippings;
using App.FakeEntity.Slide;
using App.FakeEntity.Static;
using App.FakeEntity.System;
using App.FakeEntity.User;
using AutoMapper;
using System.Linq;
using App.Core.Plugins;
using App.FakeEntity.Plugins;

namespace App.Framework.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
	{
		public override string ProfileName => "ViewModelToDomainMappings";

		public ViewModelToDomainMappingProfile()
		{
			CreateMap<RegisterFormViewModel, IdentityUser>().ForAllMembers(_ => _.Ignore());

			CreateMap<LanguageFormViewModel, Language>()
				.ForMember(x => x.LanguageName, map => map.MapFrom(vm => vm.LanguageName))
				.ForMember(x => (object)x.Id, map
				=> map.MapFrom(vm => vm.Id))
				.ForMember(x => x.LanguageCode, map
				=> map.MapFrom(vm => vm.LanguageCode))
				.ForMember(x => (object)x.Status, map
				=> map.MapFrom(vm => vm.Status))
				.ForMember(x => x.Flag, map
				=> map.Condition(source => !string.IsNullOrEmpty(source.Flag)));

			CreateMap<LocalizedPropertyViewModel, LocalizedProperty>()
			.ForMember(x => x.Id, map
			=> map.MapFrom(vm => vm.Id))
			.ForMember(x => x.EntityId, map
			=> map.MapFrom(vm => vm.EntityId))
			.ForMember(x => (object)x.LanguageId, map
			=> map.MapFrom(vm => vm.LanguageId))
			.ForMember(x => x.LocaleKeyGroup, map
			=> map.MapFrom(vm => vm.LocaleKeyGroup))
			.ForMember(x => (object)x.LocaleKey, map
			=> map.MapFrom(vm => vm.LocaleKey))
			.ForMember(x => (object)x.LocaleValue, map
			=> map.MapFrom(vm => vm.LocaleValue));

			CreateMap<ServerMailSettingViewModel, ServerMailSetting>()
				.ForMember(x => x.FromAddress,
					map => map.MapFrom(vm => vm.FromAddress))
				.ForMember(x => (object)x.Id,
					map => map.MapFrom(vm => vm.Id))
				.ForMember(x => x.SmtpClient,
					map => map.MapFrom(vm => vm.SmtpClient))
				.ForMember(x => (object)x.Status,
					map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.UserID,
					map => map.MapFrom(vm => vm.UserId))
				.ForMember(x => x.Password,
					map => map.MapFrom(vm => vm.Password))
				.ForMember(x => x.SMTPPort,
					map => map.MapFrom(vm => vm.SMTPPort))
				.ForMember(x => (object)x.EnableSSL,
					map => map.MapFrom(vm => vm.EnableSSL)).ForMember(
					x => (object)x.Status,
					map => map.MapFrom(vm => vm.Status));

			CreateMap<LandingPageViewModel, LandingPage>()
				.ForMember(x => x.FullName,
					map => map.MapFrom(vm => vm.FullName))
				.ForMember(x => (object)x.ShopId,
					map => map.MapFrom(vm => vm.ShopId))
				.ForMember(x => x.DateOfBith,
					map => map.MapFrom(vm => vm.DateOfBith))
				.ForMember(x => (object)x.Status,
					map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.PhoneNumber,
					map => map.MapFrom(vm => vm.PhoneNumber)).ForMember(
					x => x.Email, map => map.MapFrom(vm => vm.Email));

			CreateMap<ContactInformationViewModel, ContactInformation>()
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => x.Language, map => map.MapFrom(vm => vm.Language))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.Lag, map => map.MapFrom(vm => vm.Lag))
				.ForMember(x => x.Lat, map => map.MapFrom(vm => vm.Lat))
				.ForMember(x => x.NumberOfStore, map => map.MapFrom(vm => vm.NumberOfStore))
				.ForMember(x => (object)x.Type, map => map.MapFrom(vm => vm.Type))
				.ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.Email, map => map.MapFrom(vm => vm.Email))
				.ForMember(x => x.Hotline, map => map.MapFrom(vm => vm.Hotline))
				.ForMember(x => x.Address, map => map.MapFrom(vm => vm.Address))
				.ForMember(x => x.MobilePhone, map => map.MapFrom(vm => vm.MobilePhone))
				.ForMember(x => x.Fax, map => map.MapFrom(vm => vm.Fax))
				.ForMember(x => x.Province, map => map.Ignore())
				.ForMember(x => (object)x.ProvinceId, map => map.MapFrom(vm => vm.ProvinceId));

			CreateMap<SystemSettingViewModel, SystemSetting>()
				.ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.MetaTitle, map => map.MapFrom(vm => vm.MetaTitle))
				.ForMember(x => x.MetaDescription, map => map.MapFrom(vm => vm.MetaDescription))
				.ForMember(x => x.MetaKeywords, map => map.MapFrom(vm => vm.MetaKeywords))
				.ForMember(x => x.Description, map => map.MapFrom(vm => vm.Description))
				.ForMember(x => x.TimeWork, map => map.MapFrom(vm => vm.TimeWork))
				.ForMember(x => x.Hotline, map => map.MapFrom(vm => vm.Hotline))
				.ForMember(x => x.Email, map => map.MapFrom(vm => vm.Email))
				.ForMember(x => x.LogoImage, map => map.MapFrom(vm => vm.LogoImage))
				 .ForMember(x => x.LogoFooterImage, map => map.MapFrom(vm => vm.LogoFooterImage))
				.ForMember(x => x.FaviconImage, map => map.MapFrom(vm => vm.FaviconImage))
				.ForMember(x => x.Slogan, map => map.MapFrom(vm => vm.Slogan))
				.ForMember(x => x.FooterContent, map => map.MapFrom(vm => vm.FooterContent))
				.ForMember(x => (object)x.MaintanceSite, map => map.MapFrom(vm => vm.MaintanceSite));

			CreateMap<MenuLinkViewModel, MenuLink>()
				   .ForMember(x => x.MenuName, map => map.MapFrom(vm => vm.MenuName))
				   .ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				   .ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				   .ForMember(x => x.MetaTitle, map => map.MapFrom(vm => vm.MetaTitle))
				   .ForMember(x => x.MetaDescription, map => map.MapFrom(vm => vm.MetaDescription))
				   .ForMember(x => x.MetaKeywords, map => map.MapFrom(vm => vm.MetaKeywords))
				   .ForMember(x => (object)x.ParentId, map => map.MapFrom(vm => vm.ParentId))
				   .ForMember(x => x.CurrentVirtualId, map => map.Condition(source => !string.IsNullOrEmpty(source.CurrentVirtualId)))
				   .ForMember(x => x.VirtualId, map => map.Condition(source => !string.IsNullOrEmpty(source.VirtualId)))
				   .ForMember(x => (object)x.TypeMenu, map => map.MapFrom(vm => vm.TypeMenu))
				   .ForMember(x => (object)x.Position, map => map.MapFrom(vm => vm.Position))
				   .ForMember(x => (object)x.TemplateType, map => map.MapFrom(vm => vm.TemplateType))
				   .ForMember(x => (object)x.DisplayOnMenu, map => map.MapFrom(vm => vm.DisplayOnMenu))
				   .ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				   .ForMember(x => x.SourceLink, map => map.MapFrom(vm => vm.SourceLink))
				   .ForMember(x => x.SeoUrl, map => map.MapFrom(vm => vm.SeoUrl))
				   .ForMember(x => x.VirtualSeoUrl, map => map.Condition(source => !string.IsNullOrEmpty(source.VirtualSeoUrl)))
				   .ForMember(x => x.Language, map => map.MapFrom(vm => vm.Language))
				   .ForMember(x => x.ImageBigSize, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageBigSize)))
				   .ForMember(x => x.ImageMediumSize, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageMediumSize)))
				   .ForMember(x => x.ImageSmallSize, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageSmallSize)))
					.ForMember(x => x.ColorHex, map => map.Condition(source => !string.IsNullOrEmpty(source.ColorHex)))
				   .ForMember(x => x.DisplayOnHomePage, map => map.MapFrom(vm => vm.DisplayOnHomePage))
				   .ForMember(x => x.ParentMenu, map => map.Ignore())
				   .ForMember(x => x.GenericControls, map => map.Condition(source => source.GenericControls.Any()));

			CreateMap<ProvinceViewModel, Province>()
				.ForMember(x => x.Name, map => map.MapFrom(vm => vm.Name))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.OrderDisplay,
					map => map.MapFrom(vm => vm.OrderDisplay)).ForMember(
					x => (object)x.Status, map => map.MapFrom(vm => vm.Status));

			CreateMap<AttributeViewModel, Attribute>()
				.ForMember(x => x.AttributeName, map => map.MapFrom(vm => vm.AttributeName))
				.ForMember(x => (object)x.Id, map
				=> map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.OrderDisplay, map
				=> map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => x.Description, map
				=> map.MapFrom(vm => vm.Description))
				.ForMember(x => (object)x.Status, map
				=> map.MapFrom(vm => vm.Status))
				.ForMember(x => x.AttributeValues, map => map.Ignore());

			CreateMap<AttributeValueViewModel, AttributeValue>()
				.ForMember(x => x.ValueName, map => map.MapFrom(vm => vm.ValueName))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id)).ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => x.ColorHex, map => map.MapFrom(vm => vm.ColorHex))
				.ForMember(x => x.Description, map => map.MapFrom(vm => vm.Description))
				.ForMember(x => (object)x.AttributeId, map => map.MapFrom(vm => vm.AttributeId))
				.ForMember(x => x.Attribute, map => map.Ignore())
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status));

			CreateMap<GalleryImageViewModel, GalleryImage>();

			CreateMap<PostViewModel, Post>()
					.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
					.ForMember(dest => (object)dest.Id, opt => opt.MapFrom(src => src.Id))
					.ForMember(dest => (object)dest.Status, opt => opt.MapFrom(src => src.Status))
					.ForMember(dest => dest.MetaTitle, opt => opt.MapFrom(src => src.MetaTitle))
					.ForMember(dest => dest.MetaDescription, opt => opt.MapFrom(src => src.MetaDescription))
					.ForMember(dest => dest.MetaKeywords, opt => opt.MapFrom(src => src.MetaKeywords))
					.ForMember(dest => (object)dest.MenuId, opt => opt.MapFrom(src => (int)src.MenuId))
					.ForMember(dest => (object)dest.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
					.ForMember(dest => dest.TechInfo, opt => opt.MapFrom(src => src.TechInfo))
					.ForMember(dest => (object)dest.PostType, opt => opt.MapFrom(src => src.PostType))
					.ForMember(dest => (object)dest.OldOrNew, opt => opt.MapFrom(src => src.OldOrNew))
					.ForMember(dest => (object)dest.Price, opt => opt.MapFrom(src => src.Price))
					.ForMember(dest => (object)dest.Discount, opt => opt.MapFrom(src => src.Discount))

					.ForMember(dest => (object)dest.ShowOnHomePage, opt => opt.MapFrom(src => src.ShowOnHomePage))

					.ForMember(dest => (object)dest.ProductHot, opt => opt.MapFrom(src => src.ProductHot))
					.ForMember(dest => (object)dest.OutOfStock, opt => opt.MapFrom(src => src.OutOfStock))
					.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
					.ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
					.ForMember(dest => (object)dest.ProductNew, opt => opt.MapFrom(src => src.ProductNew))
					.ForMember(dest => dest.ShortDesc, opt => opt.MapFrom(src => src.ShortDesc))
					.ForMember(dest => (object)dest.OrderDisplay, opt => opt.MapFrom(src => src.OrderDisplay))
					.ForMember(dest => dest.SeoUrl, opt => opt.MapFrom(src => src.SeoUrl))
					.ForMember(dest => dest.VirtualCatUrl, opt => opt.Condition(source => !string.IsNullOrEmpty(source.VirtualCatUrl)))
					.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
					.ForMember(dest => dest.ImageBigSize, opt => opt.Condition(source => !string.IsNullOrEmpty(source.ImageBigSize)))
					.ForMember(dest => dest.ImageMediumSize, opt => opt.Condition(source => !string.IsNullOrEmpty(source.ImageMediumSize)))
					.ForMember(dest => dest.ImageSmallSize, opt => opt.Condition(source => !string.IsNullOrEmpty(source.ImageSmallSize)))
					.ForMember(dest => (object)dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
					.ForMember(dest => (object)dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
					.ForMember(dest => dest.VirtualCategoryId, opt => opt.Condition(source => !string.IsNullOrEmpty(source.VirtualCategoryId)))
					.ForMember(dest => dest.AttributeValues, opt => opt.Ignore())
					.ForMember(dest => dest.GalleryImages, opt => opt.Ignore())
					.ForMember(dest => dest.PostGallerys, opt => opt.Ignore())
					.ForMember(dest => dest.MenuLink, opt => opt.Ignore())
					.ForMember(dest => dest.Manufacturer, opt => opt.Ignore());



			CreateMap<PostGalleryViewModel, PostGallery>()
				.ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title))
				.ForMember(x => x.ImageSmallSize, map => map.MapFrom(vm => vm.ImageSmallSize))
				.ForMember(x => x.ImageBigSize, map => map.MapFrom(vm => vm.ImageBigSize))
				.ForMember(x => x.ImageMediumSize, map => map.MapFrom(vm => vm.ImageMediumSize))
				.ForMember(x => x.PostId, map => map.MapFrom(vm => vm.PostId))
				.ForMember(x => x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.IsAvatar, map => map.MapFrom(vm => vm.IsAvatar));


			CreateMap<NewsViewModel, News>()
				.ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.MetaTitle, map => map.MapFrom(vm => vm.MetaTitle))
				.ForMember(x => x.MetaDescription, map => map.MapFrom(vm => vm.MetaDescription))
				.ForMember(x => x.MetaKeywords, map => map.MapFrom(vm => vm.MetaKeywords))
				.ForMember(x => (object)x.MenuId, map => map.MapFrom(vm => vm.MenuId))
				.ForMember(x => x.Description, map => map.MapFrom(vm => vm.Description))
				.ForMember(x => x.ShortDesc, map => map.MapFrom(vm => vm.ShortDesc))
				.ForMember(x => (object)x.Video, map => map.MapFrom(vm => vm.Video))
				.ForMember(x => x.VideoLink, map => map.MapFrom(vm => vm.VideoLink))
				.ForMember(x => x.OtherLink, map => map.MapFrom(vm => vm.OtherLink))
				.ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => (object)x.SpecialDisplay, map => map.MapFrom(vm => vm.SpecialDisplay))
				.ForMember(x => (object)x.HomeDisplay, map => map.MapFrom(vm => vm.HomeDisplay))
				.ForMember(x => x.SeoUrl, map => map.MapFrom(vm => vm.SeoUrl))
				.ForMember(x => x.VirtualCatUrl, map => map.Condition(source => !string.IsNullOrEmpty(source.VirtualCatUrl)))
				.ForMember(x => x.Language, map => map.MapFrom(vm => vm.Language))
				.ForMember(x => x.ImageBigSize, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageBigSize)))
				.ForMember(x => x.ImageMediumSize, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageMediumSize)))
				.ForMember(x => x.ImageSmallSize, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageSmallSize)))
				.ForMember(x => x.VirtualCategoryId, map => map.Condition(source => !string.IsNullOrEmpty(source.VirtualCategoryId)))
				.ForMember(x => x.MenuLink, map => map.Ignore());

			CreateMap<ManufacturerViewModel, Manufacturer>()
				.ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.Description, map => map.MapFrom(vm => vm.Description))
				.ForMember(x => x.OtherLink, map => map.MapFrom(vm => vm.OtherLink))
				.ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => x.ImageUrl, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageUrl)));

			CreateMap<StaticContentViewModel, StaticContent>().
				ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title)).ForMember(x => (object)x.Id, map
					=> map.MapFrom(vm
					=> vm.Id)).ForMember(x => (object)x.Status, map
					 => map.MapFrom(vm => vm.Status)).ForMember(x
					 => x.MetaTitle, map => map.MapFrom(vm
					  => vm.MetaTitle)).ForMember(x => x.MetaDescription, map
					   => map.MapFrom(vm => vm.MetaDescription)).ForMember(x
					   => x.MetaKeywords, map
						=> map.MapFrom(vm => vm.MetaKeywords)).ForMember(x
						=> (object)x.MenuId, map => map.MapFrom(vm => vm.MenuId))
				.ForMember(x => x.Description,
					map => map.MapFrom(vm => vm.Description))
				.ForMember(x => x.ShortDesc,
					map => map.MapFrom(vm => vm.ShortDesc))
				.ForMember(x => x.SeoUrl,
					map => map.MapFrom(vm => vm.SeoUrl))
				.ForMember(x => x.Language,
					map => map.MapFrom(vm => vm.Language))
				.ForMember(x => x.ImagePath,
					map => map.Condition(source => !string.IsNullOrEmpty(source.ImagePath)))
				.ForMember(x => (object)x.ViewCount,
					map => map.MapFrom(vm => vm.ViewCount))
				.ForMember(x => x.MenuLink, map => map.Ignore());


			CreateMap<SettingSeoGlobalViewModel, SettingSeoGlobal>()
				.ForMember(x => x.FbAppId, map => map.MapFrom(vm => vm.FbAppId))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => x.FbAdminsId, map => map.MapFrom(vm => vm.FbAdminsId))
				.ForMember(x => x.SnippetGoogleAnalytics, map => map.MapFrom(vm => vm.SnippetGoogleAnalytics))
				.ForMember(x => x.MetaTagMasterTool, map => map.MapFrom(vm => vm.MetaTagMasterTool))
				.ForMember(x => x.PublisherGooglePlus, map => map.MapFrom(vm => vm.PublisherGooglePlus))
				.ForMember(x => x.FacebookRetargetSnippet, map => map.MapFrom(vm => vm.FacebookRetargetSnippet))
				.ForMember(x => x.GoogleRetargetSnippet, map => map.MapFrom(vm => vm.GoogleRetargetSnippet))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))

				.ForMember(x => x.FbLink, map => map.MapFrom(vm => vm.FbLink))
				.ForMember(x => x.GooglePlusLink, map => map.MapFrom(vm => vm.GooglePlusLink))
				.ForMember(x => x.TwitterLink, map => map.MapFrom(vm => vm.TwitterLink))
				.ForMember(x => x.PinterestLink, map => map.MapFrom(vm => vm.PinterestLink))
				.ForMember(x => x.YoutubeLink, map => map.MapFrom(vm => vm.YoutubeLink));

			CreateMap<PageBannerViewModel, PageBanner>()
				.ForMember(x => x.PageName, map => map.MapFrom(vm => vm.PageName))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => x.Language, map => map.MapFrom(vm => vm.Language))
				.ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => (object)x.Position, map => map.MapFrom(vm => vm.Position))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status));

			CreateMap<BannerViewModel, Banner>()
				.ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.WebsiteLink, map => map.MapFrom(vm => vm.WebsiteLink))
				.ForMember(x => x.ImgPath, map => map.Condition(source => !string.IsNullOrEmpty(source.ImgPath)))
				.ForMember(x => x.Width, map => map.MapFrom(vm => vm.Width))
				.ForMember(x => x.Language, map => map.MapFrom(vm => vm.Language))
				.ForMember(x => x.Height, map => map.MapFrom(vm => vm.Height))
				.ForMember(x => x.Target, map => map.MapFrom(vm => vm.Target))
				.ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => (object)x.FromDate, map => map.MapFrom(vm => vm.FromDate))
				.ForMember(x => (object)x.ToDate, map => map.MapFrom(vm => vm.ToDate))
				.ForMember(x => (object)x.PageId, map => map.MapFrom(vm => vm.PageId))
				.ForMember(x => (object)x.MenuId, map => map.MapFrom(vm => vm.MenuId))
				.ForMember(x => x.Language, map => map.MapFrom(vm => vm.Language))
				.ForMember(x => x.PageBanner, map => map.Ignore())
				.ForMember(x => x.MenuLink, map => map.Ignore());

			CreateMap<SlideShowViewModel, SlideShow>()
				.ForMember(x => x.Title, map => map.MapFrom(vm => vm.Title))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.WebsiteLink, map => map.MapFrom(vm => vm.WebsiteLink))
				.ForMember(x => x.ImgPath, map => map.Condition(source => !string.IsNullOrEmpty(source.ImgPath)))
				.ForMember(x => x.Width, map => map.MapFrom(vm => vm.Width))
				.ForMember(x => x.Description,
					map => map.MapFrom(vm => vm.Description))
				.ForMember(x => (object)x.Video,
					map => map.MapFrom(vm => vm.Video))
				.ForMember(x => x.Height, map => map.MapFrom(vm => vm.Height))
				.ForMember(x => x.Target, map => map.MapFrom(vm => vm.Target))
				.ForMember(x => (object)x.OrderDisplay,
					map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => (object)x.FromDate,
					map => map.MapFrom(vm => vm.FromDate))
				.ForMember(x => (object)x.ToDate,
					map => map.MapFrom(vm => vm.ToDate)).ForMember(
					x => (object)x.OrderDisplay,
					map => map.MapFrom(vm => vm.OrderDisplay));

			CreateMap<BrandViewModel, Brand>()
				.ForMember(x => x.Name, map => map.MapFrom(vm => vm.Name))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status));

			CreateMap<RepairViewModel, Repair>()
				.ForMember(x => x.Model, map
				=> map.MapFrom(vm => vm.Model))
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => x.ModelBrand, map => map.MapFrom(vm => vm.ModelBrand))
				.ForMember(x => x.SerialNumber, map => map.MapFrom(vm => vm.SerialNumber))
				.ForMember(x => (object)x.BrandId, map => map.MapFrom(vm => vm.BrandId))
				.ForMember(x => x.RepairCode, map => map.MapFrom(vm => vm.RepairCode))
				.ForMember(x => x.CustomerCode, map => map.Condition(source => !string.IsNullOrEmpty(source.CustomerCode)))
				.ForMember(x => x.StoreName, map => map.Condition(source => !string.IsNullOrEmpty(source.StoreName)))
				.ForMember(x => x.CustomerName, map => map.MapFrom(vm => vm.CustomerName))
				.ForMember(x => x.PhoneNumber, map => map.MapFrom(vm => vm.PhoneNumber))
				.ForMember(x => x.CustomerIdNumber, map => map.MapFrom(vm => vm.CustomerIdNumber))
				.ForMember(x => x.Address, map => map.MapFrom(vm => vm.Address))
				.ForMember(x => x.Accessories, map => map.MapFrom(vm => vm.Accessories))
				.ForMember(x => x.PasswordPhone, map => map.MapFrom(vm => vm.PasswordPhone))
				.ForMember(x => x.AppleId, map => map.MapFrom(vm => vm.AppleId))
				.ForMember(x => x.IcloudPassword, map => map.Condition(source => !string.IsNullOrEmpty(source.IcloudPassword)))
				.ForMember(x => (object)x.OldWarranty, map => map.MapFrom(source => source.OldWarranty))
				.ForMember(x => x.PhoneStatus, map => map.Condition(source => !string.IsNullOrEmpty(source.PhoneStatus)))
				.ForMember(x => x.Note, map => map.MapFrom(vm => vm.Note)).
				ForMember(x => x.RepairGalleries, map => map.Ignore())
				.ForMember(x => x.RepairItems, map => map.Ignore()).ForMember(x => x.Brand, map => map.Ignore());

			CreateMap<RepairGalleryViewModel, RepairGallery>();

			CreateMap<RepairItemViewModel, RepairItem>()
				.ForMember(x => (object)x.Id, map => map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.RepairId,
					map => map.MapFrom(vm => vm.RepairId))
				.ForMember(x => (object)x.FixedFee,
					map => map.MapFrom(vm => vm.FixedFee)).ForMember(
					x => (object)x.WarrantyFrom,
					map => map.MapFrom(vm => vm.WarrantyFrom))
				.ForMember(x => x.Repair, map => map.Ignore()).ForMember(
					x => (object)x.WarrantyTo,
					map => map.MapFrom(vm => vm.WarrantyTo));

			CreateMap<LocaleStringResourceViewModel, LocaleStringResource>()
			  .ForMember(x => x.LanguageId, map => map.MapFrom(vm => vm.LanguageId))
			  .ForMember(x => x.ResourceName, map => map.MapFrom(vm => vm.ResourceName))
			  .ForMember(x => x.ResourceValue, map => map.MapFrom(vm => vm.ResourceValue))
			  .ForMember(x => x.IsFromPlugin, map => map.MapFrom(vm => vm.IsFromPlugin))
			  .ForMember(x => x.IsTouched, map => map.MapFrom(vm => vm.IsTouched));

			CreateMap<GenericControlViewModel, GenericControl>()
				 .ForMember(x
				 => x.Name, map
				 => map.MapFrom(vm => vm.Name))
				 .ForMember(x
				 => (object)x.Id, map
				 => map.MapFrom(vm => vm.Id))
				 .ForMember(x => (object)x.OrderDisplay
				 , map => map.MapFrom(vm => vm.OrderDisplay))
				 .ForMember(x => x.Description
				 , map => map.MapFrom(vm => vm.Description))
				 .ForMember(x => (object)x.Status, map
				 => map.MapFrom(vm => vm.Status))
				 .ForMember(x => (object)x.MenuId, map
				 => map.MapFrom(vm => vm.MenuId))
				 .ForMember(x => (object)x.ControlTypeId, map
				 => map.MapFrom<int?>(vm => vm.ControlTypeId))
				 .ForMember(x => x.GenericControlValues, map => map.Ignore())
				 .ForMember(x => x.MenuLinks, map => map.Ignore());

			CreateMap<GenericControlValueViewModel, GenericControlValue>()
				.ForMember(x => x.ValueName, map
				=> map.MapFrom(vm => vm.ValueName))
				.ForMember(x => (object)x.Id, map
				=> map.MapFrom(vm => vm.Id))
				.ForMember(x => (object)x.OrderDisplay, map
				=> map.MapFrom(vm => vm.OrderDisplay))
				.ForMember(x => x.ColorHex, map
				=> map.MapFrom(vm => vm.ColorHex))
				.ForMember(x => x.Description, map
				=> map.MapFrom(vm => vm.Description))
				.ForMember(x => (object)x.GenericControlId, map
				=> map.MapFrom(vm => vm.GenericControlId))
				.ForMember(x => x.GenericControl, map => map.Ignore())
				.ForMember(x => (object)x.Status, map => map.MapFrom(vm => vm.Status))
				.ForMember(x => (object)x.EntityId, map
				=> map.MapFrom(vm => vm.EntityId));

			CreateMap<GenericControlValueItemViewModel, GenericControlValueItem>();

			CreateMap<ShoppingCartItemViewModel, ShoppingCartItem>()
			 .ForMember(x => x.StoreId, map
			 => map.MapFrom(vm => vm.StoreId))
			 .ForMember(x => (object)x.ParentItemId, map
			 => map.MapFrom(vm => vm.ParentItemId))
			 .ForMember(x => (object)x.BundleItemId, map
			 => map.MapFrom<int?>(vm => vm.BundleItemId))
			 .ForMember(x => x.ShoppingCartTypeId, map
			 => map.MapFrom(vm => vm.ShoppingCartTypeId))
			 .ForMember(x => x.CustomerId, map
			 => map.MapFrom(vm => vm.CustomerId))
			 .ForMember(x => (object)x.AttributesXml, map
			 => map.MapFrom(vm => vm.AttributesXml))
			 .ForMember(x => x.CustomerEnteredPrice, map
			 => map.MapFrom(vm => vm.CustomerEnteredPrice))
			 .ForMember(x => x.Quantity, map
			 => map.MapFrom(vm => vm.Quantity));

			CreateMap<AddressViewModel, Address>()
			  .ForMember(x => x.FirstName, map
			 => map.MapFrom(vm => vm.FirstName))
			 .ForMember(x => (object)x.LastName, map
			 => map.MapFrom(vm => vm.LastName))
			 .ForMember(x => (object)x.Email, map
			 => map.MapFrom(vm => vm.Email))
			 .ForMember(x => x.Company, map
			 => map.MapFrom(vm => vm.Company))
			 .ForMember(x => x.CountryId, map
			 => map.MapFrom(vm => vm.CountryId))
			 .ForMember(x => (object)x.StateProvinceId, map
			 => map.MapFrom(vm => vm.StateProvinceId))
			 .ForMember(x => x.City, map
			 => map.MapFrom(vm => vm.City))
			 .ForMember(x => x.Address1, map
			 => map.MapFrom(vm => vm.Address1))
			 .ForMember(x => x.Address2, map
			 => map.MapFrom(vm => vm.Address2))
			 .ForMember(x => x.ZipPostalCode, map
			 => map.MapFrom(vm => vm.ZipPostalCode))
			 .ForMember(x => x.PhoneNumber, map
			 => map.MapFrom(vm => vm.PhoneNumber))
			 .ForMember(x => x.FaxNumber, map
			 => map.MapFrom(vm => vm.FaxNumber))
			  .ForMember(x => x.Salutation, map
			 => map.MapFrom(vm => vm.Salutation))
			 .ForMember(x => x.Title, map
			 => map.MapFrom(vm => vm.Title))
			 .ForMember(x => x.Title, map
			 => map.MapFrom(vm => vm.Title));

			CreateMap<PaymentMethodViewModel, PaymentMethod>()
			   .ForMember(x => x.PaymentMethodSystemName, map => map.MapFrom(vm => vm.PaymentMethodSystemName))
			   .ForMember(x => x.Description, map => map.MapFrom(vm => vm.Description))
			   .ForMember(x => x.ImageUrl, map => map.Condition(source => !string.IsNullOrEmpty(source.ImageUrl)))
			   .ForMember(x => x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
			   .ForMember(x => x.Status, map => map.MapFrom(vm => vm.Status));

			CreateMap<ShippingMethodViewModel, ShippingMethod>()
			  .ForMember(x => x.Name, map => map.MapFrom(vm => vm.Name))
			  .ForMember(x => x.Description, map => map.MapFrom(vm => vm.Description))
			  .ForMember(x => x.OrderDisplay, map => map.MapFrom(vm => vm.OrderDisplay))
			  .ForMember(x => x.Status, map => map.MapFrom(vm => vm.Status));

			CreateMap<OrderViewModel, Order>()
			.ForMember(x => x.OrderNumber, map => map.MapFrom(vm => vm.OrderNumber))
			.ForMember(x => x.OrderGuid, map => map.MapFrom(vm => vm.OrderGuid))
			.ForMember(x => x.CreatedOnUtc, map => map.MapFrom(vm => vm.CreatedOn))
			.ForMember(x => x.BillingAddress, map => map.MapFrom(vm => vm.BillingAddress));

			CreateMap<OrderItemViewModel, OrderItem>()
		  .ForMember(x => x.OrderId, map => map.MapFrom(vm => vm.OrderId))
		  .ForMember(x => x.OrderItemGuid, map => map.MapFrom(vm => vm.OrderItemGuid))
		  .ForMember(x => x.PostId, map => map.MapFrom(vm => vm.PostId));

			CreateMap<SettingViewModel, Setting>()
				.ForMember(x => x.Name, map => map.MapFrom(vm => vm.Name))
				.ForMember(x => x.Value, map => map.MapFrom(vm => vm.Value))
				.ForMember(x => x.StoreId, map => map.MapFrom(vm => vm.StoreId));

			//plugins
			CreateMap<PluginDescriptor, PluginModel>()
				.ForMember(dest => dest.ConfigurationUrl, mo => mo.Ignore())
				.ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
				.ForMember(dest => dest.IconUrl, mo => mo.Ignore());
		}
	}
}