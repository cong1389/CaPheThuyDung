using System.IO;
using System.Net;

namespace App.Front.Models
{
    public class SpeedSmsapi
	{
		public const int TypeQc = 1;

		public const int TypeCskh = 2;

		public const int TypeBrandname = 3;

		private const string RootUrl = "http://api.speedsms.vn/index.php";

		private string _accessToken = "Dz9QCXHaRUvVHw8k_gXapFDvVlR83Ps6";

		public SpeedSmsapi()
		{
		}

		public SpeedSmsapi(string token)
		{
			_accessToken = token;
		}

		public string GetUserInfo()
		{
			string str = "http://api.speedsms.vn/index.php/user/info";
			NetworkCredential networkCredential = new NetworkCredential(_accessToken, ":x");
			return (new StreamReader((new WebClient
			{
				Credentials = networkCredential
			}).OpenRead(str))).ReadToEnd();
		}

		public string SendSms(string phone, string content, int type, string brandname)
		{
			string str = "http://api.speedsms.vn/index.php/sms/send";
			if (phone.Length <= 0 || phone.Length < 10 || phone.Length > 11)
			{
				return "";
			}
			if (content.Equals(""))
			{
				return "";
			}
			if (type < 1 || type > 3)
			{
				return "";
			}
			if (type == 3 && brandname.Equals(""))
			{
				return "";
			}
			if (!brandname.Equals("") && brandname.Length > 11)
			{
				return "";
			}
			NetworkCredential networkCredential = new NetworkCredential(_accessToken, ":x");
			WebClient webClient = new WebClient
			{
				Credentials = networkCredential
			};
			webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
			return webClient.UploadString(str, string.Concat("{\"to\":[\"", phone, "\"], \"content\": \"", content, "\", \"type\":", type, ", \"brandname\": \"", brandname, "\"}"));
		}
	}
}