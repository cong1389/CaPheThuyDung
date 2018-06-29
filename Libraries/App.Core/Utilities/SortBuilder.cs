namespace App.Core.Utilities
{
    public class SortBuilder
	{
		public string ColumnName
		{
			get;
			set;
		}

		public SortOrder ColumnOrder
		{
			get;
			set;
		}

		public SortBuilder()
		{
		}

		public enum SortOrder
		{
			Ascending,
			Descending
		}
	}
}