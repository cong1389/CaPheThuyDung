using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Infra.Data.Common;
using App.Infra.Data.Repository.MailSetting;
using App.Infra.Data.UOW.Interfaces;

namespace App.Service.MailSetting
{
	public class MailSettingService : BaseService<ServerMailSetting>, IMailSettingService
	{
		private readonly IMailSettingRepository _mailSettingRepository;

		private readonly IUnitOfWork _unitOfWork;

		public MailSettingService(IUnitOfWork unitOfWork, IMailSettingRepository mailSettingRepository) : base(unitOfWork, mailSettingRepository)
		{
			_unitOfWork = unitOfWork;
			_mailSettingRepository = mailSettingRepository;
		}

		public ServerMailSetting GetById(int id)
		{
			return _mailSettingRepository.GetById(id);
		}

		public ServerMailSetting GetActive()
		{
			return _mailSettingRepository.FindBy(m => m.Status == 1).FirstOrDefault();
		}

		public IEnumerable<ServerMailSetting> PagedList(SortingPagingBuilder sortbuBuilder, Paging page)
		{
			return _mailSettingRepository.PagedSearchList(sortbuBuilder, page);
		}

		public int Save()
		{
			return _unitOfWork.Commit();
		}
	}
}