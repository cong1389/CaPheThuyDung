using App.Domain.Entities.Language;
using App.FakeEntity.Language;
using App.Service.Common;
using App.Service.LocaleStringResource;
using AutoMapper;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace App.Admin.Areas.Admin.Hubs
{
    [HubName("webGridHub")]
    public class WebGridHub : Hub
    {
        private readonly ILocaleStringResourceService _localeStringResourceService;

        private readonly ICommonServices _services;

        public WebGridHub(ICommonServices services, ILocaleStringResourceService localeStringResourceService)
        {
            this._services = services;
            this._localeStringResourceService = localeStringResourceService;
        }

        public Task SaveRecord(LocaleStringResource locale)
        {
            //LocaleStringResource province = Mapper.Map<LocaleStringResourceViewModel, LocaleStringResource>(locale);
            _localeStringResourceService.Update(locale);
            //var record = _repository.GetById(locale.Id);
            //record.UserName = locale.UserName;
            //record.FirstName = locale.FirstName;
            //record.LastName = locale.LastName;
            //record.LastLogin = locale.LastLogin;

            //_repository.SaveChanges();

            return Clients.Caller.recordSaved();
        }
    }
}