using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using App.Core.Extensions;
using App.Core.Utilities;

namespace App.Core.Infrastructure
{
	public static class EngineContext
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static IEngine Initialize(bool forceRecreate, IEngine engine = null)
		{
			if (Singleton<IEngine>.Instance == null || forceRecreate)
			{
				Singleton<IEngine>.Instance = engine ?? CreateEngineInstance();
				Singleton<IEngine>.Instance.Initialize();
			}
			return Singleton<IEngine>.Instance;
		}

		/// <summary>
		/// Creates a factory instance and adds a http application injecting facility.
		/// </summary>
		/// <returns>A new factory</returns>
		public static IEngine CreateEngineInstance()
		{
			var engineTypeSetting = CommonHelper.GetAppSetting<string>("sm:EngineType");
			if (engineTypeSetting.HasValue())
			{
				var engineType = Type.GetType(engineTypeSetting);

				if (engineType == null)
				{
					throw new ConfigurationErrorsException("The type '" + engineType + "' could not be found. Please check the configuration at /configuration/appSettings/add[@key=sm:EngineType] or check for missing assemblies.");
				}

				if (!typeof(IEngine).IsAssignableFrom(engineType))
				{
					throw new ConfigurationErrorsException("The type '" + engineType + "' doesn't implement 'SmartStore.Core.Infrastructure.IEngine' and cannot be configured in /configuration/appSettings/add[@key=sm:EngineType] for that purpose.");
				}

				return Activator.CreateInstance(engineType) as IEngine;
			}

		    return new GoWebEngine();
		}

		public static IEngine Current
		{
			get
			{
				if (Singleton<IEngine>.Instance == null)
				{
					Initialize(false);
				}
				return Singleton<IEngine>.Instance;
			}
		}
	}
}
