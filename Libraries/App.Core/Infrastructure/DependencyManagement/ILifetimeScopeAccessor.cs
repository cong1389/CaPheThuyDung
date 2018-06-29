using System;
using Autofac;

namespace App.Core.Infrastructure.DependencyManagement
{
	public interface ILifetimeScopeAccessor
	{
		ILifetimeScope ApplicationContainer { get; }
		IDisposable BeginContextAwareScope();
		void EndLifetimeScope();
		ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction);
	}
}