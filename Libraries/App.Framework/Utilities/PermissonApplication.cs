using System;
using System.Web.Mvc;

namespace App.Framework.Utilities
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class PermissonApplication : AuthorizeAttribute
	{
	}
}