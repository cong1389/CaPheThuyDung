using App.Core.Localization;

namespace App.Service.Language
{
    public interface ITextService
    {
        LocalizedString Get(string key, params object[] args);
    }
}
