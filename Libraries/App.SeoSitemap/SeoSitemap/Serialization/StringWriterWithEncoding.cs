using System.IO;
using System.Text;

namespace App.SeoSitemap.Serialization
{
    internal sealed class StringWriterWithEncoding : StringWriter
	{
		public override Encoding Encoding
		{
			get;
		}

		public StringWriterWithEncoding(Encoding encoding)
		{
			Encoding = encoding;
		}
	}
}