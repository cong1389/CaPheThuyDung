using System.Text;
using System.Xml;
using App.SeoSitemap.Enum;
using App.SeoSitemap.StyleSheets;

namespace App.SeoSitemap.Serialization
{
    internal class XmlProcessingInstructionHandler : IXmlProcessingInstructionHandler
	{
		public XmlProcessingInstructionHandler()
		{
		}

		public void AddStyleSheets(XmlWriter xmlWriter, IHasStyleSheets model)
		{
			if (model.StyleSheets == null)
			{
				return;
			}
			foreach (XmlStyleSheet styleSheet in model.StyleSheets)
			{
				StringBuilder stringBuilder = new StringBuilder($@"type=""{styleSheet.Type}"" href=""{styleSheet.Url}""");
				WriteAttribute(stringBuilder, "title", styleSheet.Title);
				WriteAttribute(stringBuilder, "media", styleSheet.Media);
				WriteAttribute(stringBuilder, "charset", styleSheet.Charset);
				if (styleSheet.Alternate.HasValue && styleSheet.Alternate.Value != YesNo.None)
				{
					YesNo value = styleSheet.Alternate.Value;
					WriteAttribute(stringBuilder, "alternate", value.ToString().ToLowerInvariant());
				}
				xmlWriter.WriteProcessingInstruction("xml-stylesheet", stringBuilder.ToString());
			}
		}

		private void WriteAttribute(StringBuilder stringBuilder, string attributeName, string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				stringBuilder.Append($@" {attributeName}=""{value}""");
			}
		}
	}
}