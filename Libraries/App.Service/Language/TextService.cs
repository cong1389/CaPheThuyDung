using App.Core.Localization;
using App.Service.LocaleStringResource;

namespace App.Service.Language
{
    public class TextService: ITextService
    {
        private readonly ILocaleStringResourceService _localizationService;

        public TextService(ILocaleStringResourceService localizationService)
        {
            _localizationService = localizationService;
        }

        public LocalizedString Get(string key, params object[] args)
        {
            try
            {
                var value = _localizationService.GetResource(key);
               
                if (string.IsNullOrEmpty(value))
                {
                    return new LocalizedString(key);
                }

                if (args == null || args.Length == 0)
                {
                    return new LocalizedString(value);
                }

                return new LocalizedString(string.Format(value, args), key, args);
            }
            catch
            {
                // ignored
            }

            return new LocalizedString(key);
        }
    }
}
