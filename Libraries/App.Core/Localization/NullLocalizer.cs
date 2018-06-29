namespace App.Core.Localization
{
    public static class NullLocalizer
    {
        private static readonly Localizer s_instance;

        static NullLocalizer()
        {
            s_instance = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));
        }

        public static Localizer Instance => s_instance;
    }
}
