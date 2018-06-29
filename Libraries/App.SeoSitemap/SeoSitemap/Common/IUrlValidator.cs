namespace App.SeoSitemap.Common
{
    internal interface IUrlValidator
	{
		void ValidateUrls(object item, IBaseUrlProvider baseUrlProvider);
	}
}