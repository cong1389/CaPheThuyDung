using App.Core.Messages;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace App.Core.Utilities
{
	public static class MessageTemplateConverter
	{
		public static MessageTemplate Load(string templateName)
		{
			var rootPath = CommonHelper.MapPath($"~/Themes/Basic/TemplateMails/{templateName}.xml");

			if (!File.Exists(rootPath))
			{
				throw new FileNotFoundException($"File '{rootPath}' does not exist.");
			}

			return DeserializeTemplate(rootPath);
		}

		private static DirectoryInfo ResolveTemplateDirectory()
		{
			var rootPath = CommonHelper.MapPath("~/App_Data/EmailTemplates/");

			return new DirectoryInfo(rootPath);

		}

		private static MessageTemplate DeserializeTemplate(string fullPath)
		{
			MessageTemplate template = null;
			try
			{
				template = DeserializeDocument(XDocument.Load(fullPath));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return template;
		}

		private static MessageTemplate DeserializeDocument(XDocument doc)
		{
			var root = doc.Root;
			var result = new MessageTemplate();

			foreach (var node in root.Nodes().OfType<XElement>())
			{
				var value = node.Value.Trim();

				switch (node.Name.LocalName)
				{
					case "To":
						result.To = value;
						break;
					case "ReplyTo":
						result.ReplyTo = value;
						break;
					case "Subject":
						result.Subject = value;
						break;
					case "ModelTypes":
						result.ModelTypes = value;
						break;
					case "Body":
						result.Body = value;
						break;
				}
			}

			return result;
		}
	}

}
