namespace App.Infra.Data.DbFactory
{
    public class DbFactory : Disposable, IDbFactory
	{
		private Context.AppContext _dbContext;

		public DbFactory()
		{
		}

		protected override void DisposeCore()
		{
		    _dbContext?.Dispose();
		}

		public Context.AppContext Init()
		{
			Context.AppContext appContext = _dbContext;

		    if (appContext != null) return appContext;

		    Context.AppContext appContext1 = new Context.AppContext();
		    Context.AppContext appContext2 = appContext1;

		    _dbContext = appContext1;
		    appContext = appContext2;

		    return appContext;
		}
	}
}