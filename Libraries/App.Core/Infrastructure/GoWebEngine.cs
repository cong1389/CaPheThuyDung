using App.Core.Infrastructure.DependencyManagement;
using App.Core.Plugins;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Core.Infrastructure
{
    public class GoWebEngine : IEngine
    {
        private ContainerManager _containerManager;

        public ContainerManager ContainerManager => _containerManager;

        public void Initialize()
        {
            RegisterDependencies();

            //if (DataSettings.DatabaseIsInstalled())
            //{
            //    RunStartupTasks();
            //}
        }

        protected virtual ITypeFinder CreateTypeFinder()
        {
            return new WebAppTypeFinder();
        }

        protected virtual ContainerManager RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            var typeFinder = CreateTypeFinder();

            _containerManager = new ContainerManager(container);

            // core dependencies
            builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IEngine>();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>();

            // Autofac
            var lifetimeScopeAccessor = new DefaultLifetimeScopeAccessor(container);
            var lifetimeScopeProvider = new DefaultLifetimeScopeProvider(lifetimeScopeAccessor);
            builder.RegisterInstance(lifetimeScopeAccessor).As<ILifetimeScopeAccessor>();
            builder.RegisterInstance(lifetimeScopeProvider).As<ILifetimeScopeProvider>();

            var dependencyResolver = new AutofacDependencyResolver(container, lifetimeScopeProvider);
            builder.RegisterInstance(dependencyResolver);
            DependencyResolver.SetResolver(dependencyResolver);

#pragma warning disable 612, 618
            builder.Update(container);
#pragma warning restore 612, 618

            // Register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var registrarTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var registrarInstances = new List<IDependencyRegistrar>();
            foreach (var type in registrarTypes)
            {
                registrarInstances.Add((IDependencyRegistrar)Activator.CreateInstance(type));
            }

            // Sort
            registrarInstances = registrarInstances.OrderBy(t => t.Order).ToList();
            foreach (var registrar in registrarInstances)
            {
                var type = registrar.GetType();
                registrar.Register(builder, typeFinder, PluginManager.IsActivePluginAssembly(type.Assembly));
            }

#pragma warning disable 612, 618
            builder.Update(container);
#pragma warning restore 612, 618

            return _containerManager;
        }

    }
}
