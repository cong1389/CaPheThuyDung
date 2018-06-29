using App.Infra.Data.DbFactory;
using App.Infra.Data.UOW.Interfaces;

namespace App.UnitOfWork.UOW
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbFactory _dbFactory;

		private Infra.Data.Context.AppContext _dbContext;

		public Infra.Data.Context.AppContext DbContext
		{
			get
			{
				Infra.Data.Context.AppContext appContext = _dbContext;
				if (appContext == null)
				{
					Infra.Data.Context.AppContext appContext1 = _dbFactory.Init();
					Infra.Data.Context.AppContext appContext2 = appContext1;
					_dbContext = appContext1;
					appContext = appContext2;
				}
				return appContext;
			}
		}

		public UnitOfWork(IDbFactory dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public int Commit()
		{
			return DbContext.Commit();
		}
	}
}