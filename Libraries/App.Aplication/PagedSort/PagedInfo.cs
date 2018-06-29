using System;
using System.Linq;
using App.Aplication.PagedSort.SortUtils;

namespace App.Aplication.PagedSort
{
    public class PagedInfo : IPagedInfo
	{
		public const int MaxSortSpecifications = 3;

		public const int DefaultPageSize = 20;

		public static readonly int[] PageSizes;

		private int _mPageNo = 1;

		private int _mTotalItems;

		private int _mPageSize = 20;

		public bool IsFirstPage => PageNo <= 1;

	    public bool IsLastPage => PageNo >= TotalPages;

	    public int PageNo
		{
			get => JustDecompileGenerated_get_PageNo();
	        set => JustDecompileGenerated_set_PageNo(value);
	    }

		public int JustDecompileGenerated_get_PageNo()
		{
			return _mPageNo;
		}

		public void JustDecompileGenerated_set_PageNo(int value)
		{
			_mPageNo = (value < 1 ? 1 : value);
		}

		public int PageSize
		{
			get => _mPageSize;
		    set
			{
				int num = value;
				if (!PageSizes.Contains(value))
				{
					num = PageSizes.FirstOrDefault(size => size > value);
					if (num < 1)
					{
						num = PageSizes.Last();
					}
				}
				_mPageSize = num;
			}
		}

		public string SortMetaData { get; set; } = string.Empty;

		public int TotalItems
		{
			get => _mTotalItems;
		    set => _mTotalItems = value < 0 ? 0 : value;
		}

		public int TotalPages => (int)Math.Ceiling(decimal.Parse( TotalItems.ToString()) / PageSize);

	    static PagedInfo()
		{
			PageSizes = new[] { 5, 10, 20, 50, 100 };
		}

		public PagedInfo()
		{
		}

		public PagedInfo(string sortTitle, string sortExpression) : this(sortTitle, sortExpression, SortDirection.Ascending)
		{
		}

		public PagedInfo(string sortTitle, string sortExpression, SortDirection sortDirection)
		{
			AddSortExpression(sortTitle, sortExpression, sortDirection);
		}

		public PagedInfo AddSortExpression(string metaData)
		{
			AddSortExpression(SortExpression.DeSerialize(metaData));
			return this;
		}

		public PagedInfo AddSortExpression(string title, string sortExpression, SortDirection direction  )
		{
			AddSortExpression(new SortExpression(title, sortExpression, direction));
			return this;
		}

		public PagedInfo AddSortExpression(SortExpression sortExpression)
		{
			SortMetaData = AddSortExpression(SortMetaData, sortExpression);
			return this;
		}

		public static string AddSortExpression(string sortMetaData, SortExpression sortExpression)
		{
			SortExpressionCollection sortExpressions = GetSortExpressions(sortMetaData);
			int num = sortExpressions.FindIndex(s => s.Expression == sortExpression.Expression);
			if (num != 0)
			{
				if (num > 0)
				{
					sortExpressions.RemoveAt(num);
				}
				sortExpressions.Insert(0, sortExpression);
				if (sortExpressions.Count > 3)
				{
					sortExpressions.RemoveRange(3, 1);
				}
			}
			else
			{
				sortExpressions[0].ToggleDirection();
			}
			return sortExpressions.Serialize();
		}

		public void ClearSortExpressions()
		{
			SortMetaData = string.Empty;
		}

		public static int GetNearestPageSize(int targetSize)
		{
			for (int i = 0; i < PageSizes.Length; i++)
			{
				int pageSizes = PageSizes[i];
				if (pageSizes > targetSize)
				{
					if (i == 0)
					{
						return pageSizes;
					}
					int num = PageSizes[i - 1];
					if (targetSize - num >= pageSizes - targetSize)
					{
						return pageSizes;
					}
					return num;
				}
			}
			return PageSizes.Last();
		}

		public string GetSortDescription()
		{
			return GetSortDescription(SortMetaData);
		}

		public static string GetSortDescription(string sortMetaData)
		{
			return GetSortExpressions(sortMetaData).ToString();
		}

		public SortExpressionCollection GetSortExpressions()
		{
			return GetSortExpressions(SortMetaData);
		}

		public static SortExpressionCollection GetSortExpressions(string sortMetaData)
		{
			return SortExpressionCollection.DeSerialize(sortMetaData);
		}
	}
}