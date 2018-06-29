using System;

namespace App.Infra.Data.DbFactory
{
	public interface IDbFactory : IDisposable
	{
		Context.AppContext Init();
	}
}