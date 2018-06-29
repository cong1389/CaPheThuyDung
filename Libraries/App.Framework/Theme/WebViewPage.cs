using System.Web.Mvc;
using App.Service.Language;

namespace App.Framework.Theme
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private ITextService _textService;

        protected virtual ITextService TextService => _textService ?? (_textService = DependencyResolver.Current.GetService<ITextService>());

        //private WebViewPageHelper _helper;

        //private Localizer t;
        /// <summary>
        /// Get a localized resource
        /// </summary>
        public MvcHtmlString T(string text, params object[] formatterArguments)
        {
            var translated = TextService.Get(text, formatterArguments);
            return
                MvcHtmlString.Create(translated);
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            //t = NullLocalizer.Instance;
            //_helper = DependencyResolver.Current.GetService<WebViewPageHelper>();
            ////_helper.Initialize(this.ViewContext);
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
