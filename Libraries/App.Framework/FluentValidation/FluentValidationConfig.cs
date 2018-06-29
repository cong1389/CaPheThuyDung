using System;
using System.Collections.Generic;
using App.FakeEntity.Ads;
using App.FakeEntity.Attribute;
using App.FakeEntity.Brandes;
using App.FakeEntity.Common;
using App.FakeEntity.ContactInformation;
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
using App.FakeEntity.ServerMail;
using App.FakeEntity.Slide;
using App.FakeEntity.Static;
using App.FakeEntity.System;
using App.FakeEntity.User;
using App.Framework.ValidateEntity;
using FluentValidation;

namespace App.Framework.FluentValidation
{
    public class FluentValidationConfig : ValidatorFactoryBase
	{
		private readonly Dictionary<Type, IValidator> _validators;

		public FluentValidationConfig()
		{
			_validators = new Dictionary<Type, IValidator>();
			AddBinding();
		}

		public void AddBinding()
		{
			_validators.Add(typeof(IValidator<LanguageFormViewModel>), new LanguageValidator());
			_validators.Add(typeof(IValidator<ServerMailSettingViewModel>), new MailSettingValidator());
			_validators.Add(typeof(IValidator<ContactInformationViewModel>), new ContactInformationValidator());
			_validators.Add(typeof(IValidator<SystemSettingViewModel>), new SystemSettingValidator());
			_validators.Add(typeof(IValidator<MenuLinkViewModel>), new MenuLinkValidator());
			_validators.Add(typeof(IValidator<ProvinceViewModel>), new ProvinceValidator());
			_validators.Add(typeof(IValidator<DistrictViewModel>), new DistrictValidator());
			_validators.Add(typeof(IValidator<PostViewModel>), new PostValidator());

            _validators.Add(typeof(IValidator<PostGalleryViewModel>), new PostGalleryValidator());

            _validators.Add(typeof(IValidator<NewsViewModel>), new NewsValidator());
			_validators.Add(typeof(IValidator<StaticContentViewModel>), new StaticContentValidator());
			_validators.Add(typeof(IValidator<BannerViewModel>), new BannerValidator());
			_validators.Add(typeof(IValidator<LoginViewModel>), new LoginValidator());
			_validators.Add(typeof(IValidator<ChangePasswordViewModel>), new ChangePasswordValidator());
			_validators.Add(typeof(IValidator<SlideShowViewModel>), new SlideShowValidator());
			_validators.Add(typeof(IValidator<AttributeViewModel>), new AttributeValidator());
			_validators.Add(typeof(IValidator<AttributeValueViewModel>), new AttributeValueValidator());
			_validators.Add(typeof(IValidator<LandingPageViewModel>), new LandingPageValidator());
            _validators.Add(typeof(IValidator<ManufacturerViewModel>), new ManufacturerValidator());
            _validators.Add(typeof(IValidator<BrandViewModel>), new BrandValidator());
            _validators.Add(typeof(IValidator<RepairViewModel>), new RepairValidator());
            _validators.Add(typeof(IValidator<LocalizedPropertyViewModel>), new LocalizedPropertyValidator());

            _validators.Add(typeof(IValidator<GenericControlViewModel>), new GenericControlValidator());
            _validators.Add(typeof(IValidator<GenericControlValueViewModel>), new GenericControlValueValidator());

            _validators.Add(typeof(IValidator<ShoppingCartItemViewModel>), new ShoppingCartItemValidator());

            _validators.Add(typeof(IValidator<AddressViewModel>), new AddressValidator());

            _validators.Add(typeof(IValidator<PaymentMethodViewModel>), new PaymentMethodValidator());
        }

		public override IValidator CreateInstance(Type validatorType)
		{
            return !_validators.TryGetValue(validatorType, out var validator) ? null : validator;
		}
	}
}