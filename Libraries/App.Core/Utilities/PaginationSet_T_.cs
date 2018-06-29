using System.Collections.Generic;
using System.Linq;

namespace App.Core.Utilities
{
    public class PaginationSet<T>
	{
		public int Count => Items?.Count() ?? 0;

	    public IEnumerable<T> Items
		{
			get;
			set;
		}

		public int Page
		{
			get;
			set;
		}

		public int TotalCount
		{
			get;
			set;
		}

		public int TotalPages
		{
			get;
			set;
		}

		public PaginationSet()
		{
		}
	}
}