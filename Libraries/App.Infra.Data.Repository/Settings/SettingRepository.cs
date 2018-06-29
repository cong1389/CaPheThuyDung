using App.Core.Utilities;
using App.Domain.Entities.Setting;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;
using System.Collections.Generic;
using System.Linq;

namespace App.Infra.Data.Repository.Settings
{
	public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
	{
		public SettingRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<Setting> GetDefaultOrder(IQueryable<Setting> query)
		{
			var Settings = 
				from p in query
				orderby p.Id
				select p;
			return Settings;
		}

		public IEnumerable<Setting> PagedList(Paging page)
		{
			return GetAllPagedList(page).ToList();
		}

		public IEnumerable<Setting> PagedSearchList(SortingPagingBuilder sortBuider, Paging page)
		{
			var expression = PredicateBuilder.True<Setting>();
			return FindAndSort(expression, sortBuider.Sorts, page);
		}
	}
}