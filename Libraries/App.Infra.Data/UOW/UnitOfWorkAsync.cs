using App.Infra.Data.DbFactory;
using App.Infra.Data.UOW.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infra.Data.UOW
{
	public class UnitOfWorkAsync : IUnitOfWorkAsync
	{
		private readonly IDbFactory _dbFactory;

		private Context.AppContext _dbContext;

		public Context.AppContext DbContext
		{
			get
			{
				Context.AppContext appContext = _dbContext;
				if (appContext == null)
				{
					Context.AppContext appContext1 = _dbFactory.Init();
					Context.AppContext appContext2 = appContext1;
					_dbContext = appContext1;
					appContext = appContext2;
				}
				return appContext;
			}
		}

		public UnitOfWorkAsync(IDbFactory dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public Task<int> CommitAsync()
		{
			return DbContext.CommitAsync();
		}

		public Task<int> CommitAsync(CancellationToken cancellationToken)
		{
			return DbContext.CommitAsync();
		}
	}
}