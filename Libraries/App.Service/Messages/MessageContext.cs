using App.Core.Localization;
using App.Domain.Entities.GlobalSetting;
using Domain.Entities.Customers;
using System;
using System.Globalization;
using App.Core.Messages;

namespace App.Service.Messages
{
	public class MessageContext
    {
	    private IFormatProvider _formatProvider;

	    public MessageTemplate MessageTemplate { get; set; }

		public string MessageTemplateName { get; set; }

        /// <summary>
        /// If <c>null</c>, the email account specifies the sender.
        /// </summary>
        public ServerMailSetting SenderEmailAddress { get; set; }

        /// <summary>
        /// If <c>null</c>, obtained from WorkContext.CurrentCustomer.
        /// </summary>
        public Customer Customer { get; set; }

		/// <summary>
		/// If <c>null</c>, obtained from WorkContext.WorkingLanguage.
		/// </summary>
		public int? LanguageId { get; set; }

		/// <summary>
		/// If <c>null</c>, obtained from StoreContext.CurrentStore.
		/// </summary>
		public int? StoreId { get; set; }

		internal Domain.Entities.Language.Language Language { get; set; }
        //internal Store Store { get; set; }
        public ServerMailSetting EmailAccount { get; internal set; }

        public SystemSetting SystemSettings { get; internal set; }

        public bool TestMode { get; set; }
		
		public Uri BaseUri { get; set; }

        /// <summary>
        /// The final template model containing all global and template specific model parts.
        /// </summary>
        public TemplateModel Model { get; set; }

        /// <summary>
        /// If <c>null</c>, inferred from <see cref="LanguageId"/>.
        /// </summary>
        public IFormatProvider FormatProvider
        {
            get
            {
                if (_formatProvider == null)
                {
                    var culture = this.Language?.LanguageCode;
                    if (culture != null && LocalizationHelper.IsValidCultureCode(culture))
                    {
                        _formatProvider = CultureInfo.GetCultureInfo(culture);
                    }
                }

                return _formatProvider ?? CultureInfo.CurrentCulture;
            }
            set => _formatProvider = value;
        }

        private IFormatProvider GetFormatProvider(MessageContext messageContext)
        {
            var culture = messageContext.Language.LanguageCode;

            if (LocalizationHelper.IsValidCultureCode(culture))
            {
                return CultureInfo.GetCultureInfo(culture);
            }

            return CultureInfo.CurrentCulture;
        }

        public static MessageContext Create(string messageTemplateName, int languageId, int? storeId = null, Customer customer = null)
		{
			return new MessageContext
			{
				MessageTemplateName = messageTemplateName,
				LanguageId = languageId,
				StoreId = storeId,
				Customer = customer
			};
		}
	}
}
