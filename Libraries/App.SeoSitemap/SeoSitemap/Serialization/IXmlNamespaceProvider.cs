using System.Collections.Generic;

namespace App.SeoSitemap.Serialization
{
    internal interface IXmlNamespaceProvider
	{
		IEnumerable<string> GetNamespaces();
	}
}