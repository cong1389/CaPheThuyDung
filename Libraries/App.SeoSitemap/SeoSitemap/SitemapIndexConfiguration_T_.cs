using System.Collections.Generic;
using System.Linq;
using App.SeoSitemap.StyleSheets;

namespace App.SeoSitemap
{
    public abstract class SitemapIndexConfiguration<T> : ISitemapIndexConfiguration<T>
	{
		public int? CurrentPage
		{
			get;
		}

		public IQueryable<T> DataSource
		{
			get;
		}

		public List<XmlStyleSheet> SitemapIndexStyleSheets
		{
			get => GetSitemapIndexStyleSheets();
		    set => SetSitemapIndexStyleSheets(value);
		}

		private List<XmlStyleSheet> _justDecompileGeneratedSitemapIndexStyleSheetsKBackingField;

		public List<XmlStyleSheet> GetSitemapIndexStyleSheets()
		{
			return _justDecompileGeneratedSitemapIndexStyleSheetsKBackingField;
		}

		protected void SetSitemapIndexStyleSheets(List<XmlStyleSheet> value)
		{
			_justDecompileGeneratedSitemapIndexStyleSheetsKBackingField = value;
		}

		public List<XmlStyleSheet> SitemapStyleSheets
		{
			get => GetSitemapStyleSheets();
		    set => GetSitemapStyleSheets(value);
		}

		private List<XmlStyleSheet> _generatedSitemapStyleSheetsKBackingField;

		public List<XmlStyleSheet> GetSitemapStyleSheets()
		{
			return _generatedSitemapStyleSheetsKBackingField;
		}

		protected void GetSitemapStyleSheets(List<XmlStyleSheet> value)
		{
			_generatedSitemapStyleSheetsKBackingField = value;
		}

		public int Size
		{
			get => GetSize();
		    set => SetSize(value);
		}

		private int _generatedSizeKBackingField;

		public int GetSize()
		{
			return _generatedSizeKBackingField;
		}

		protected void SetSize(int value)
		{
			_generatedSizeKBackingField = value;
		}

		public bool UseReverseOrderingForSitemapIndexNodes
		{
			get => GetUseReverseOrderingForSitemapIndexNodes();
		    set => JustDecompileGenerated_set_UseReverseOrderingForSitemapIndexNodes(value);
		}

		private bool _generatedUseReverseOrderingForSitemapIndexNodesKBackingField;

		public bool GetUseReverseOrderingForSitemapIndexNodes()
		{
			return _generatedUseReverseOrderingForSitemapIndexNodesKBackingField;
		}

		protected void JustDecompileGenerated_set_UseReverseOrderingForSitemapIndexNodes(bool value)
		{
			_generatedUseReverseOrderingForSitemapIndexNodesKBackingField = value;
		}

		protected SitemapIndexConfiguration(IQueryable<T> dataSource, int? currentPage)
		{
			DataSource = dataSource;
			CurrentPage = currentPage;
			Size = 50000;
		}

		public abstract SitemapNode CreateNode(T source);

		public abstract SitemapIndexNode CreateSitemapIndexNode(int currentPage);
	}
}