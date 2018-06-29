using System;
using System.Collections.Generic;
using System.Linq;
using App.Core.Extensions;
using App.Core.Plugins.Providers;
using Autofac;

namespace App.Framework.Plugins
{
	public class ProviderManager : IProviderManager
	{
		private readonly IComponentContext _ctx;

		public ProviderManager(IComponentContext ctx)
		{
			_ctx = ctx;
		}

		public Provider<TProvider> GetProvider<TProvider>(string systemName) where TProvider : IProvider
		{
			if (systemName.IsEmpty())
				return null;

			var provider = _ctx.ResolveOptionalNamed<Lazy<TProvider, ProviderMetadata>>(systemName);
			if (provider != null)
			{
			    var d = provider.Metadata.PluginDescriptor;
			    if (d != null)
			    {
			        return null;
			    }

                SetUserData(provider.Metadata);
				return new Provider<TProvider>(provider);
			}

			return null;
		}

		public Provider<IProvider> GetProvider(string systemName)
		{
			var provider = _ctx.ResolveOptionalNamed<Lazy<IProvider, ProviderMetadata>>(systemName);
			if (provider != null)
			{
			    var d = provider.Metadata.PluginDescriptor;
			    if (d != null)
			    {
			        return null;
			    }

                SetUserData(provider.Metadata);
				return new Provider<IProvider>(provider);
			}
			return null;
		}

		public IEnumerable<Provider<TProvider>> GetAllProviders<TProvider>() where TProvider : IProvider
		{
			var providers = _ctx.Resolve<IEnumerable<Lazy<TProvider, ProviderMetadata>>>();
		    providers = from p in providers
		        let d = p.Metadata.PluginDescriptor
		        where d == null
		        select p;
            return SortProviders(providers.Select(x => new Provider<TProvider>(x)));
		}

		public IEnumerable<Provider<IProvider>> GetAllProviders()
		{
			var providers = _ctx.Resolve<IEnumerable<Lazy<IProvider, ProviderMetadata>>>();
		    providers = from p in providers
		        let d = p.Metadata.PluginDescriptor
		        where d == null
		        select p;

            return SortProviders(providers.Select(x => new Provider<IProvider>(x)));
		}

		protected virtual IEnumerable<Provider<TProvider>> SortProviders<TProvider>(IEnumerable<Provider<TProvider>> providers) where TProvider : IProvider
		{
			foreach (var m in providers.Select(x => x.Metadata))
			{
				SetUserData(m);
			}

			return providers.OrderBy(x => x.Metadata.DisplayOrder).ThenBy(x => x.Metadata.FriendlyName);
		}

		protected virtual void SetUserData(ProviderMetadata metadata)
		{
			if (!metadata.IsEditable)
			{
				return;
			}

			var displayOrder = string.Empty;
			var name = string.Empty;
			var description = string.Empty;
			metadata.FriendlyName = name;
			metadata.Description = description;
		}
	}
}
