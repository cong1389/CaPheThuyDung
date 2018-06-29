using System.Collections.Generic;

namespace App.Service.Language
{
    public interface ILocalizedModel
    {
    }

    public interface ILocalizedModel<TLocalizedModel> : ILocalizedModel
    {
        IList<TLocalizedModel> Locales { get; set; }
    }
}
