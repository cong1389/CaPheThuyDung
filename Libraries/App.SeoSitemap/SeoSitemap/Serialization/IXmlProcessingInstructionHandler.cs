using System.Xml;
using App.SeoSitemap.StyleSheets;

namespace App.SeoSitemap.Serialization
{
    internal interface IXmlProcessingInstructionHandler
	{
		void AddStyleSheets(XmlWriter xmlWriter, IHasStyleSheets model);
	}
}