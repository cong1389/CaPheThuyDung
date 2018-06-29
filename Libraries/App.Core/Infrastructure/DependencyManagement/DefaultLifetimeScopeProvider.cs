using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Mvc;

namespace App.Core.Infrastructure.DependencyManagement
{
    public class DefaultLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly ILifetimeScopeAccessor _accessor;

        public DefaultLifetimeScopeProvider(ILifetimeScopeAccessor accessor)
        {
            this._accessor = accessor;
            AutofacRequestLifetimeHttpModule.SetLifetimeScopeProvider(this);
        }

        public ILifetimeScope ApplicationContainer
        {
            get { return _accessor.ApplicationContainer; }
        }

        public void EndLifetimeScope()
        {
            _accessor.EndLifetimeScope();
        }

        public ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return _accessor.GetLifetimeScope(configurationAction);
        }

    }
}
