using System.Collections.Generic;
using System.Xml.Serialization;

namespace App.SeoSitemap.Serialization
{
    internal interface IXmlNamespaceBuilder
	{
		XmlSerializerNamespaces Create(IEnumerable<string> namespaces);
	}
}