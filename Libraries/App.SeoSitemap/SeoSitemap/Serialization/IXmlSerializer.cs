using System.IO;

namespace App.SeoSitemap.Serialization
{
    internal interface IXmlSerializer
	{
		string Serialize<T>(T data);

		void SerializeToStream<T>(T data, Stream stream);
	}
}