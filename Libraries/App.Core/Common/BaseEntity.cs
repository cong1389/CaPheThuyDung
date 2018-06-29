using System;
using System.Diagnostics.CodeAnalysis;

namespace App.Core.Common
{
    public abstract class BaseEntity
	{
        protected BaseEntity()
		{
		}

	    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
	    public Type GetUnproxiedType()
	    {
	        var t = GetType();
	        if (t.AssemblyQualifiedName.StartsWith("System.Data.Entity."))
	        {
	            // it's a proxied type
	            t = t.BaseType;
	        }

	        return t;
	    }
    }
}