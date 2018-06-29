using System;
using System.ComponentModel;

namespace App.Aplication.PagedSort.SortUtils
{
    [Serializable]
	[TypeConverter(typeof(SortExpressionConverter))]
	public class SortExpression
	{
		public SortDirection Direction
		{
			get;
			set;
		}

		public string Expression
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public SortExpression()
		{
			Title = "";
			Expression = "";
			Direction = SortDirection.Ascending;
		}

		public SortExpression(string title, string sortExpression, SortDirection direction ) : this()
		{
			Title = title;
			Expression = sortExpression;
			Direction = direction;
		}

		public static SortExpression DeSerialize(string data)
		{
			return (SortExpression)TypeDescriptor.GetConverter(typeof(SortExpression)).ConvertFrom(data);
		}

		public string Serialize()
		{
			return Serialize(this);
		}

		public static string Serialize(SortExpression sortExpression)
		{
			return (string)TypeDescriptor.GetConverter(typeof(SortExpression)).ConvertTo(sortExpression, typeof(string));
		}

		public void ToggleDirection()
		{
			Direction = (Direction == SortDirection.Descending ? SortDirection.Ascending : SortDirection.Descending);
		}

		public override string ToString()
		{
			return $"{Title} ({Direction})";
		}
	}
}