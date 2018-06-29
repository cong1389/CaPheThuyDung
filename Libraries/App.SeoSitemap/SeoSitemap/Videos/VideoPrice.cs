using System.Xml.Serialization;

namespace App.SeoSitemap.Videos
{
    public class VideoPrice
	{
		[XmlAttribute("currency")]
		public string Currency
		{
			get;
			set;
		}

		[XmlAttribute("resolution")]
		public VideoPurchaseResolution Resolution
		{
			get;
			set;
		}

		[XmlAttribute("type")]
		public VideoPurchaseOption Type
		{
			get;
			set;
		}

		[XmlText]
		public decimal Value
		{
			get;
			set;
		}

		internal VideoPrice()
		{
		}

		public VideoPrice(string currency, decimal value)
		{
			Currency = currency;
			Value = value;
		}

		public bool ShouldSerializeResolution()
		{
			return Resolution != VideoPurchaseResolution.None;
		}

		public bool ShouldSerializeType()
		{
			return Type != VideoPurchaseOption.None;
		}
	}
}