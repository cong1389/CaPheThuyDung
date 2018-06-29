using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Plugins.Providers
{
	public interface IProviderManager
	{
		Provider<TProvider> GetProvider<TProvider>(string systemName) where TProvider : IProvider;

		Provider<IProvider> GetProvider(string systemName);

		IEnumerable<Provider<TProvider>> GetAllProviders<TProvider>() where TProvider : IProvider;

		IEnumerable<Provider<IProvider>> GetAllProviders();
	}
}
