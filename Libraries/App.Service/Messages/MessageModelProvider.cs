using System;
using System.Collections.Generic;
using System.Dynamic;
using App.Core.Common;
using App.Core.ComponentModel;
using App.Core.Extensions;
using App.Core.IO.VirtualPath;
using App.Core.Templating;
using App.Domain.Common;
using App.Domain.Entities.GlobalSetting;
using App.Domain.Orders;
using App.Service.Addresses;
using Domain.Entities.Customers;

namespace App.Service.Messages
{
	public partial class MessageModelProvider : IMessageModelProvider
    {
        private readonly IVirtualPathProvider _vpp;

	    public MessageModelProvider(IVirtualPathProvider vpp)
        {
            _vpp = vpp;
        }

        public virtual void AddGlobalModelParts(MessageContext messageContext)
        {
            var model = messageContext.Model;

            model["Context"] = new Dictionary<string, object>
            {
                { "TemplateName", messageContext.MessageTemplate.Name },
                { "LanguageId", messageContext.Language.Id },
                { "LanguageCulture", messageContext.Language.LanguageCode },
                { "BaseUrl", messageContext.BaseUri }
            };

            dynamic email = new ExpandoObject();
            email.Email = messageContext.EmailAccount.FromAddress;
            email.SenderName = messageContext.EmailAccount.UserID;
            email.DisplayName = messageContext.EmailAccount.UserID; // Alias
            model["Email"] = email;
            model["Theme"] = CreateThemeModelPart(messageContext);
            model["Customer"] = CreateModelPart(messageContext.Customer, messageContext);
            model["Store"] = CreateModelPart(messageContext.SystemSettings, messageContext);
        }

        public virtual void AddModelPart(object part, MessageContext messageContext, string name = null)
        {
            var model = messageContext.Model;

            name = name.NullEmpty() ?? ResolveModelName(part);

            object modelPart = null;

            switch (part)
            {
                case INamedModelPart x:
                    modelPart = x;
                    break;
                case IModelPart x:
                    MergeModelBag(x, model, messageContext);
                    break;
                case Order x:
                    modelPart = CreateModelPart(x, messageContext);
                    break;
	            case Domain.Entities.Data.Post x:
		            modelPart = CreateModelPart(x, messageContext);
		            break;
			}

            if (modelPart != null)
            {
                if (name.IsEmpty())
                {
                    throw new Exception($"Could not resolve a model key for part '{modelPart.GetType().Name}'. Use an instance of 'NamedModelPart' class to pass model with name.");
                }

                if (model.TryGetValue(name, out var existing))
                {
                    // A model part with the same name exists in model already...
                    if (existing is IDictionary<string, object> x)
                    {
                        // but it's a dictionary which we can easily merge with
                        x.Merge(FastProperty.ObjectToDictionary(modelPart));
                    }
                    else
                    {
                        // Wrap in HybridExpando and merge
                        var he = new HybridExpando(existing, true);
                        he.Merge(FastProperty.ObjectToDictionary(modelPart));
                        model[name] = he;
                    }
                }
                else
                {
                    // Put part to model as new property
                    model[name] = modelPart;
                }
            }
        }

        public string ResolveModelName(object model)
        {
            string name = null;
            var type = model.GetType();

            try
            {
                if (model is BaseEntity be)
                {
                    name = be.GetUnproxiedType().Name;
                }
                else if (model is ITestModel te)
                {
                    name = te.ModelName;
                }
                else if (model is INamedModelPart mp)
                {
                    name = mp.ModelPartName;
                }
                else if (type.IsPlainObjectType())
                {
                    name = type.Name;
                }
            }
            catch { }

            return name;
        }

        protected virtual void MergeModelBag(IModelPart part, IDictionary<string, object> model, MessageContext messageContext)
        {
            if (!(model.Get("Bag") is IDictionary<string, object> bag))
            {
                model["Bag"] = bag = new Dictionary<string, object>();
            }

            var source = part as IDictionary<string, object>;
            bag.Merge(source);
        }

        protected virtual object CreateThemeModelPart(MessageContext messageContext)
        {
            var m = new Dictionary<string, object>
            {
                { "FontFamily", "-apple-system, system-ui, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif" },
                { "BodyBg", "#f2f4f6" },
                { "BodyColor", "#555" },
                { "TitleColor", "#2f3133" },
                { "ContentBg", "#fff" },
                { "ShadeColor", "#e2e2e2" },
                { "LinkColor", "#0066c0" },
                { "BrandPrimary", "#3f51b5" },
                { "BrandSuccess", "#4caf50" },
                { "BrandWarning", "#ff9800" },
                { "BrandDanger", "#f44336" },
                { "MutedColor", "#a5a5a5" },
            };

            return m;
        }

        protected virtual object CreateModelPart(MessageContext messageContext)
        {
            var host = messageContext.BaseUri.ToString();
            //var logoInfo = _services.PictureService.GetPictureInfo(messageContext.Store.LogoPictureId);

            // Issue: https://github.com/smartstoreag/SmartStoreNET/issues/1321

            var m = new Dictionary<string, object>
            {
                { "Email", messageContext.EmailAccount.FromAddress },
                { "EmailName", messageContext.EmailAccount.FromAddress },
                { "Name", "" },
                { "Url", host },
                { "Cdn", "" },
                { "PrimaryStoreCurrency", "" },
                { "PrimaryExchangeRateCurrency","" },
                { "Logo", "" },
                { "Company", "" },
                { "Contact","" },
                { "Bank", "" },
                { "Copyright", "" }
            };

            return m;
        }

        protected virtual object CreateModelPart(Address part, MessageContext messageContext)
        {
            var salutation = part.Salutation.NullEmpty();
            var title = part.Title.NullEmpty();
            var company = part.Company;
            var firstName = part.FirstName.NullEmpty();
            var lastName = part.LastName.NullEmpty();
            var street1 = part.Address1;
            var street2 = part.Address2;
            var zip = part.ZipPostalCode;
            var city = part.City;
           
            var m = new Dictionary<string, object>
            {
                { "Title", title },
                { "Salutation", salutation },
                { "FullName", part.GetFullName().NullEmpty() },
                { "Company", company },
                { "FirstName", firstName },
                { "LastName", lastName },
                { "Street1", street1 },
                { "Street2", street2 },
                { "City", city },
                { "ZipCode", zip },
                { "Email", part.Email.NullEmpty() },
                { "Phone", part.PhoneNumber },
                { "Fax", part.FaxNumber }
            };
            
            return m;
        }

        protected virtual object CreateModelPart(SystemSetting part, MessageContext messageContext)
        {
            var host = messageContext.BaseUri.ToString();

            var m = new Dictionary<string, object>
            {
                { "Title", part.Title},
                { "LogoSrc",_vpp.Combine(host, part.LogoImage) },
            };

            return m;
        }

        protected virtual object CreateModelPart(Domain.Entities.Data.Post part, MessageContext messageContext, string attributesXml = null)
        {
            var name = part.Title;

            var m = new Dictionary<string, object>
            {
                { "Id", part.Id },
                { "Name", name },
                { "Description", part.ShortDesc.NullEmpty() },
                { "DeliveryTime", null },
                { "QtyUnit", null }
            };
            
            return m;
        }

		protected virtual object CreateModelPart(Customer part, MessageContext messageContext)
		{
			var m = new Dictionary<string, object>
			{
				["Id"] = part.Id,
				["CustomerGuid"] = part.CustomerGuid,
				["Username"] = part.Username,
				["Email"] = part.Email,
				["IsTaxExempt"] = part.IsTaxExempt,
				["LastIpAddress"] = part.LastIpAddress,
				["CreatedOn"] = part.CreatedOnUtc,
				["LastLoginOn"] = part.LastLoginDateUtc,
				["LastActivityOn"] = part.LastActivityDateUtc,

				["FullName"] = part.Username,
				
				// Addresses
				["BillingAddress"] = CreateModelPart(part.BillingAddress ?? new Address(), messageContext),
				["ShippingAddress"] = part.ShippingAddress,
			};
			

			return m;
		}
	}
}
