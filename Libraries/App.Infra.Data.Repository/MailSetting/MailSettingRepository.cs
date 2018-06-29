using System.Collections.Generic;
using System.Linq;
using App.Core.Utilities;
using App.Domain.Entities.GlobalSetting;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.MailSetting
{
    public class MailSettingRepository : RepositoryBase<ServerMailSetting>, IMailSettingRepository
	{
		public MailSettingRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public ServerMailSetting GetById(int id)
		{
			var serverMailSetting = FindBy(x => x.Id == id).FirstOrDefault();
			return serverMailSetting;
		}

		protected override IOrderedQueryable<ServerMailSetting> GetDefaultOrder(IQueryable<ServerMailSetting> query)
		{
			var serverMailSettings = 
				from p in query
				orderby p.Id
				select p;
			return serverMailSettings;
		}

		public IEnumerable<ServerMailSetting> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<ServerMailSetting> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<ServerMailSetting>();
			if (!string.IsNullOrEmpty(sortBuider.Keywords))
			{
				expression = expression.And(x => x.FromAddress.ToLower().Contains(sortBuider.Keywords.ToLower()) || x.UserID.ToLower().Contains(sortBuider.Keywords.ToLower()));
			}
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}