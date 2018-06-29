using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App.Aplication.PagedSort.SortUtils
{
	[Serializable]
	[TypeConverter(typeof(SortExpressionCollectionConverter))]
	public class SortExpressionCollection : List<SortExpression>
	{
		public SortExpressionCollection()
		{
		}

		public SortExpressionCollection(string title, string sortExpression, SortDirection direction ) : this(new SortExpression(title, sortExpression, direction))
		{
		}

		public SortExpressionCollection(SortExpression sortBy) : this()
		{
			Add(sortBy);
		}

		public static SortExpressionCollection DeSerialize(string data)
		{
			return (SortExpressionCollection)TypeDescriptor.GetConverter(typeof(SortExpressionCollection)).ConvertFrom(data);
		}

		public string Serialize()
		{
			return Serialize(this);
		}

		public static string Serialize(SortExpressionCollection collection)
		{
			return (string)TypeDescriptor.GetConverter(typeof(SortExpressionCollection)).ConvertTo(collection, typeof(string));
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SortExpression sortExpression in this)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(sortExpression);
			}
			return stringBuilder.ToString();
		}
	}
}