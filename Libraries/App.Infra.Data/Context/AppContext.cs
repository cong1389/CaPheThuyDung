using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Common;
using App.Domain.Common;
using App.Domain.Entities.Account;
using App.Domain.Entities.Ads;
using App.Domain.Entities.Attribute;
using App.Domain.Entities.Brandes;
using App.Domain.Entities.Data;
using App.Domain.Entities.GenericControl;
using App.Domain.Entities.GlobalSetting;
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
using App.Infra.Data.Mapping;
using Domain.Entities.Customers;

namespace App.Infra.Data.Context
{
    public class AppContext : DbContext
    {
        public virtual IDbSet<Domain.Entities.Attribute.Attribute> Attributes
        {
            get;
            set;
        }

        public virtual IDbSet<AttributeValue> AttributeValues
        {
            get;
            set;
        }

        public virtual IDbSet<Banner> Banners
        {
            get;
            set;
        }

        public virtual IDbSet<ContactInformation> ContactInformations
        {
            get;
            set;
        }

        public virtual IDbSet<Manufacturer> Manufacturers
        {
            get;
            set;
        }

        public virtual IDbSet<GalleryImage> GalleryImages
        {
            get;
            set;
        }

        public virtual IDbSet<LandingPage> LandingPages
        {
            get;
            set;
        }

        public virtual IDbSet<Language> Languages
        {
            get;
            set;
        }

        public virtual IDbSet<ExternalLogin> Logins
        {
            get;
            set;
        }

        public virtual IDbSet<MenuLink> MenuLinks
        {
            get;
            set;
        }

        public virtual IDbSet<News> News
        {
            get;
            set;
        }

        public virtual IDbSet<PageBanner> PageBanners
        {
            get;
            set;
        }

        public virtual IDbSet<Post> Posts
        {
            get;
            set;
        }

        public virtual IDbSet<PostGallery> PostsGallerys
        {
            get;
            set;
        }

        public virtual IDbSet<Province> Provinces
        {
            get;
            set;
        }

        public virtual IDbSet<Role> Roles
        {
            get;
            set;
        }

        public virtual IDbSet<ServerMailSetting> ServerMailSettings
        {
            get;
            set;
        }

        public virtual IDbSet<SettingSeoGlobal> SettingSeoGlobals
        {
            get;
            set;
        }

        public virtual IDbSet<SlideShow> SlideShows
        {
            get;
            set;
        }

        public virtual IDbSet<StaticContent> StaticContents
        {
            get;
            set;
        }

        public virtual IDbSet<SystemSetting> SystemSettings
        {
            get;
            set;
        }

        public virtual IDbSet<User> Users
        {
            get;
            set;
        }

        public virtual IDbSet<Brand> Brand
        {
            get;
            set;
        }

        public virtual IDbSet<Repair> Repair
        {
            get;
            set;
        }

        public virtual IDbSet<RepairGallery> RepairGallery
        {
            get;
            set;
        }

        public virtual IDbSet<RepairItem> RepairItem
        {
            get;
            set;
        }

        public virtual IDbSet<LocalizedProperty> LocalizedProperty
        {
            get;
            set;
        }

        public virtual IDbSet<GenericAttribute> GenericAttribute
        {
            get;
            set;
        }

        public virtual IDbSet<LocaleStringResource> LocaleStringResource
        {
            get;
            set;
        }

        public virtual IDbSet<GenericControl> GenericControls
        {
            get;
            set;
        }

        public virtual IDbSet<GenericControlValue> GenericControlValues
        {
            get;
            set;
        }
        public virtual IDbSet<GenericControlValueItem> GenericControlValueItems
        {
            get;
            set;
        }

        public virtual IDbSet<ShoppingCartItem> ShoppingCartItems
        {
            get;
            set;
        }

        public virtual IDbSet<Customer> Customers
        {
            get;
            set;
        }

        public virtual IDbSet<Address> Address
        {
            get;
            set;
        }

        public virtual IDbSet<PaymentMethod> PaymentMethods
        {
            get;
            set;
        }

        public virtual IDbSet<ShippingMethod> ShippingMethods
        {
            get;
            set;
        }

        public virtual IDbSet<Order> Order
        {
            get;
            set;
        }
        public virtual IDbSet<OrderItem> OrderItem
        {
            get;
            set;
        }

	    public virtual IDbSet<Setting> Setting
	    {
		    get;
		    set;
	    }

		public AppContext() : base("AppConnect")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual int Commit()
        {
            var dbEntityEntries =
                from x in ChangeTracker.Entries()
                where x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)
                select x;

            foreach (var dbEntityEntry in dbEntityEntries)
            {
                if (dbEntityEntry.Entity is IAuditableEntity entity)
                {
                    string name = Thread.CurrentPrincipal.Identity.Name;
                    DateTime utcNow = DateTime.UtcNow;
                    if (dbEntityEntry.State != EntityState.Added)
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }
                    else
                    {
                        entity.CreatedBy = name;
                        entity.CreatedDate = utcNow;
                    }
                    entity.UpdatedBy = name;
                    entity.UpdatedDate = utcNow;
                }
            }
            return SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            var dbEntityEntries =
                from x in ChangeTracker.Entries()
                where x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)
                select x;

            foreach (var dbEntityEntry in dbEntityEntries)
            {
                if (dbEntityEntry.Entity is IAuditableEntity entity)
                {
                    string name = Thread.CurrentPrincipal.Identity.Name;
                    DateTime utcNow = DateTime.UtcNow;
                    if (dbEntityEntry.State != EntityState.Added)
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }
                    else
                    {
                        entity.CreatedBy = name;
                        entity.CreatedDate = utcNow;
                    }
                    entity.UpdatedBy = name;
                    entity.UpdatedDate = utcNow;
                }
            }
            return SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Properties().Where(x => x.Name == string.Concat(x.ReflectedType.Name, "Id")).Configure(x =>
                x.IsKey().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new ClaimConfiguration());
            modelBuilder.Configurations.Add(new MenuLinkConfiguration());
            modelBuilder.Configurations.Add(new BannerConfiguration());
            modelBuilder.Configurations.Add(new ContactInformationConfiguration());
            modelBuilder.Configurations.Add(new LandingPageConfiguration());

            modelBuilder.Configurations.Add(new PostsConfiguration());
            modelBuilder.Configurations.Add(new PostsGalleryConfiguration());

            modelBuilder.Configurations.Add(new AttribureValueConfiguration());
            modelBuilder.Configurations.Add(new NewsConfiguration());
            modelBuilder.Configurations.Add(new GalleryImageConfiguration());
            modelBuilder.Configurations.Add(new StaticContentConfiguration());
            modelBuilder.Configurations.Add(new ManufacturerConfiguration());
            modelBuilder.Configurations.Add(new BrandConfiguration());
            modelBuilder.Configurations.Add(new RepairConfiguration());
            modelBuilder.Configurations.Add(new RepairGalleryConfiguration());
            modelBuilder.Configurations.Add(new RepairItemConfiguration());
            modelBuilder.Configurations.Add(new LocalizedPropertyConfiguration());

            modelBuilder.Configurations.Add(new GenericAttributeConfiguration());
            modelBuilder.Configurations.Add(new LocaleStringResourceConfiguration());

            modelBuilder.Configurations.Add(new GenericControlConfiguration());
            modelBuilder.Configurations.Add(new GenericControlValueConfiguration());
            modelBuilder.Configurations.Add(new GenericControlValueItemConfiguration());

            modelBuilder.Configurations.Add(new ShoppingCartItemConfiguration());

            modelBuilder.Configurations.Add(new CustomerConfiguration());

            modelBuilder.Configurations.Add(new AddressConfiguration());

            modelBuilder.Configurations.Add(new PaymentMethodConfiguration());
            modelBuilder.Configurations.Add(new ShippingMethodConfiguration());

            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderItemConfiguration());
            

        }
    }
}