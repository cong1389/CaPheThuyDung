using App.Aplication.PagedSort.SortUtils;

namespace App.Aplication.PagedSort
{
    public interface IPagedInfo
	{
		bool IsFirstPage
		{
			get;
		}

		bool IsLastPage
		{
			get;
		}

		int PageNo
		{
			get;
		}

		int PageSize
		{
			get;
			set;
		}

		string SortMetaData
		{
			get;
			set;
		}

		int TotalItems
		{
			get;
			set;
		}

		int TotalPages
		{
			get;
		}

		PagedInfo AddSortExpression(string metaData);

		PagedInfo AddSortExpression(string title, string sortExpression, SortDirection direction);

		PagedInfo AddSortExpression(SortExpression sortExpression);

		void ClearSortExpressions();

		string GetSortDescription();

		SortExpressionCollection GetSortExpressions();
	}
}