using System.Collections.Generic;

namespace App.Aplication
{
	public class ModelHelper<T>
	{
		public IEnumerable<T> Result
		{
			get;
			set;
		}

		public int TotalRecords
		{
			get;
			set;
		}
	}
}