using App.Framework.FluentValidation;
using App.Framework.Ioc;
using App.Framework.Mappings;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using FluentValidation.Mvc;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

public class Bootstrapper
{
    public static ContainerBuilder Run()
    {
        return ContainerBuilder.Value;
    }

    private static readonly Lazy<ContainerBuilder> ContainerBuilder = new Lazy<ContainerBuilder>(() =>
    {
        var container = new ContainerBuilder();
        SetAutofacContainer(container);
        AutoMapperConfiguration.Configure();
        FluentValidationModelValidatorProvider.Configure(provider => provider.ValidatorFactory = new FluentValidationConfig());

        return container;
    });
    
    private static void SetAutofacContainer(ContainerBuilder containerBuilder)
    {
        //ContainerBuilder containerBuilder = new ContainerBuilder();
        Assembly[] array = (
            from Assembly p in BuildManager.GetReferencedAssemblies()
            where p.ManifestModule.Name.StartsWith("App.")
            select p).ToArray();

        containerBuilder.RegisterControllers(array);
        containerBuilder.RegisterAssemblyModules(array);
        //containerBuilder.RegisterApiControllers(array);
        containerBuilder.RegisterFilterProvider();
        containerBuilder.RegisterSource(new ViewRegistrationSource());
        containerBuilder.RegisterModule(new EFModule());
        containerBuilder.RegisterModule(new RepositoryModule());
        containerBuilder.RegisterModule(new IdentityModule());
        containerBuilder.RegisterModule(new ServiceModule());

        IContainer container = containerBuilder.Build(ContainerBuildOptions.IgnoreStartableComponents);
        //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
}