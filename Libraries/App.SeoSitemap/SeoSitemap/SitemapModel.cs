using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using App.SeoSitemap.Serialization;
using App.SeoSitemap.StyleSheets;

namespace App.SeoSitemap
{
    [XmlRoot("urlset", Namespace="http://www.sitemaps.org/schemas/sitemap/0.9")]
	public class SitemapModel : IXmlNamespaceProvider, IHasStyleSheets
	{
		[XmlElement("url")]
		public List<SitemapNode> Nodes
		{
			get;
		}

		[XmlIgnore]
		public List<XmlStyleSheet> StyleSheets
		{
			get;
			set;
		}

		internal SitemapModel()
		{
		}

		public SitemapModel(List<SitemapNode> nodes)
		{
			Nodes = nodes;
		}

		public IEnumerable<string> GetNamespaces()
		{
			if (Nodes == null)
			{
				yield break;
			}
			List<SitemapNode> nodes = Nodes;

			List<SitemapNode> sitemapNodes = Nodes;
			if (sitemapNodes.Any(node => node.News != null))
			{
				yield return "http://www.google.com/schemas/sitemap-news/0.9";
			}
			List<SitemapNode> nodes1 = Nodes;
			if (nodes1.Any(node => node.Video != null))
			{
				yield return "http://www.google.com/schemas/sitemap-video/1.1";
			}
			List<SitemapNode> sitemapNodes1 = Nodes;
			if (sitemapNodes1.Any(node => node.Mobile != null))
			{
				yield return "http://www.google.com/schemas/sitemap-mobile/1.0";
			}
			List<SitemapNode> nodes2 = Nodes;
			if (nodes2.Any(node => {
				if (node.Translations == null)
				{
					return false;
				}
				return node.Translations.Any();
			}))
			{
				yield return "http://www.w3.org/1999/xhtml";
			}
		}
	}
}