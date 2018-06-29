using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;

namespace App.Framework.Templating.Liquid.Drops
{
	internal class EnumerableWrapper : IEnumerable<ILiquidizable>, ISafeObject
	{
		private readonly IEnumerable _enumerable;

		public EnumerableWrapper(IEnumerable enumerable)
		{
			_enumerable = enumerable;
		}

		public IEnumerator GetEnumerator()
		{
			return GetEnumeratorInternal();
		}

		public object GetWrappedObject() => _enumerable;

		IEnumerator<ILiquidizable> IEnumerable<ILiquidizable>.GetEnumerator()
		{
			return GetEnumeratorInternal();
		}

		private IEnumerator<ILiquidizable> GetEnumeratorInternal()
		{
			return _enumerable
				.Cast<object>()
				.Select(x => LiquidUtil.CreateSafeObject(x))
				.OfType<ILiquidizable>()
				.GetEnumerator();
		}
	}
}
