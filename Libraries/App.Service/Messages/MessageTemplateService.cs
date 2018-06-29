using App.Core.Caching;

namespace App.Service.Messages
{
	public class MessageTemplateService
    {
        private const string MESSAGETEMPLATES_BY_NAME_KEY = "SmartStore.messagetemplate.name-{0}-{1}";
        private readonly IRequestCache _requestCache;

        public MessageTemplateService(IRequestCache requestCache)
        {
            _requestCache = requestCache;
        }

        //public virtual MessageTemplate GetMessageTemplateByName(string messageTemplateName, int storeId)
        //{
        //    if (string.IsNullOrWhiteSpace(messageTemplateName))
        //    {
        //        throw new ArgumentException("messageTemplateName");
        //    }

        //    string key = string.Format(MESSAGETEMPLATES_BY_NAME_KEY, messageTemplateName, storeId);
        //    return _requestCache.Get(key, () =>
        //    {
        //        var query = _messageTemplateRepository.Table;
        //        query = query.Where(t => t.Name == messageTemplateName);
        //        query = query.OrderBy(t => t.Id);
        //        var templates = query.ToList();

        //        //store mapping
        //        if (storeId > 0)
        //        {
        //            return templates.Where(t => _storeMappingService.Authorize(t, storeId)).FirstOrDefault();
        //        }

        //        return templates.FirstOrDefault();
        //    });

        //}
    }
}
