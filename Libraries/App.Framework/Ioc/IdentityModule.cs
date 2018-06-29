using System;
using App.Domain.Entities.Identity;
using App.Service.Account;
using App.Service.MailSetting;
using App.Service.Messages;
using Autofac;
using Microsoft.AspNet.Identity;

namespace App.Framework.Ioc
{
    public class IdentityModule : Module
	{
	    protected override void Load(ContainerBuilder builder)
		{
            builder.RegisterType<UserManager<IdentityUser, Guid>>().As<UserManager<IdentityUser, Guid>>().InstancePerRequest();
            builder.RegisterType<UserStoreService>().As<IUserStore<IdentityUser, Guid>>().InstancePerRequest();
            builder.RegisterType<RoleManager<IdentityRole, Guid>>().As<RoleManager<IdentityRole, Guid>>().InstancePerRequest();
			builder.RegisterType<RoleStoreService>().As<IRoleStore<IdentityRole, Guid>>().InstancePerRequest();

		    builder.RegisterType<SendMailService>().As<IIdentityMessageService>().InstancePerRequest();
        }
	}
}