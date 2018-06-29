using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Domain.Entities.Account;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.Account
{
    public class ExternalLoginRepository : RepositoryBaseAsync<ExternalLogin>, IExternalLoginRepository
	{
		public ExternalLoginRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public ExternalLogin GetByProviderAndKey(string loginProvider, string providerKey)
		{
			var externalLogin = Get(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
			return externalLogin;
		}

		public Task<ExternalLogin> GetByProviderAndKeyAsync(string loginProvider, string providerKey)
		{
			var async = GetAsync(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
			return async;
		}

		public Task<ExternalLogin> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey)
		{
			var async = GetAsync(cancellationToken, x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
			return async;
		}

		protected override IOrderedQueryable<ExternalLogin> GetDefaultOrder(IQueryable<ExternalLogin> query)
		{
			var externalLogins = 
				from p in query
				orderby p.UserId
				select p;
			return externalLogins;
		}
	}
}