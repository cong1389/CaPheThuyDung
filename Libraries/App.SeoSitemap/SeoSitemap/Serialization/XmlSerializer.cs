using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using App.SeoSitemap.StyleSheets;

namespace App.SeoSitemap.Serialization
{
    internal class XmlSerializer : IXmlSerializer
	{
		private readonly IXmlNamespaceBuilder _xmlNamespaceBuilder;

		private readonly XmlProcessingInstructionHandler _xmlProcessingInstructionHandler;

		public XmlSerializer()
		{
			_xmlNamespaceBuilder = new XmlNamespaceBuilder();
			_xmlProcessingInstructionHandler = new XmlProcessingInstructionHandler();
		}

		public string Serialize<T>(T data)
		{
			StringWriter stringWriterWithEncoding = new StringWriterWithEncoding(Encoding.UTF8);
			SerializeToStream(data, settings => XmlWriter.Create(stringWriterWithEncoding, settings));
			return stringWriterWithEncoding.ToString();
		}

		public void SerializeToStream<T>(T data, Stream stream)
		{
			SerializeToStream(data, settings => XmlWriter.Create(stream, settings));
		}

		private void SerializeToStream<T>(T data, Func<XmlWriterSettings, XmlWriter> createXmlWriter)
		{
		    IXmlNamespaceProvider xmlNamespaceProvider = data as IXmlNamespaceProvider;
			var xmlProvider = xmlNamespaceProvider != null ? xmlNamespaceProvider.GetNamespaces() : Enumerable.Empty<string>();
			XmlSerializerNamespaces xmlSerializerNamespace = _xmlNamespaceBuilder.Create(xmlProvider);
			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
			using (XmlWriter xmlWriter = createXmlWriter(new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				NamespaceHandling = NamespaceHandling.OmitDuplicates
			}))
			{
				if (data is IHasStyleSheets)
				{
					_xmlProcessingInstructionHandler.AddStyleSheets(xmlWriter, data as IHasStyleSheets);
				}
				xmlSerializer.Serialize(xmlWriter, data, xmlSerializerNamespace);
			}
		}
	}
}