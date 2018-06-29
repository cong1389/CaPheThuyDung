using App.Infra.Data.DbFactory;
using App.Infra.Data.UOW;
using App.Infra.Data.UOW.Interfaces;
using Autofac;

namespace App.Framework.Ioc
{
	public class EFModule : Module
	{
	    protected override void Load(ContainerBuilder builder)
		{
            builder.RegisterType<UnitOfWork.UOW.UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<UnitOfWorkAsync>().As<IUnitOfWorkAsync>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
        }
	}
}