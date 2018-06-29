using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace App.SeoSitemap.News
{
	public class SitemapNews
	{
		[XmlElement("access", Order=2)]
		public NewsAccess? Access
		{
			get;
			set;
		}

		[XmlElement("genres", Order=3)]
		public string Genres
		{
			get;
			set;
		}

		[XmlElement("keywords", Order=6)]
		public string Keywords
		{
			get;
			set;
		}

		[XmlElement("publication", Order=1)]
		public NewsPublication Publication
		{
			get;
			set;
		}

		[XmlElement("publication_date", Order=4)]
		public DateTime PublicationDate
		{
			get;
			set;
		}

		[XmlElement("stock_tickers", Order=7)]
		public string StockTickers
		{
			get;
			set;
		}

		[XmlElement("title", Order=5)]
		public string Title
		{
			get;
			set;
		}

		internal SitemapNews()
		{
		}

		public SitemapNews(NewsPublication newsPublication, DateTime publicationDate, string title)
		{
			Publication = newsPublication;
			PublicationDate = publicationDate;
			Title = title;
		}

		public bool ShouldSerializeAccess()
		{
			return Access.HasValue;
		}
	}
}