using App.Core.Utilities;
using App.Domain.Entities.Setting;
using App.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace App.Service.Settings
{
	public interface ISettingService : IBaseService<Setting>
	{
		void SetSetting<T>(string key, T value, bool cache = true);
		T GetSetting<T>(
			string key,
			T defaultValue = default(T),
			int storeId = 0,
			bool loadSharedValueIfNotFound = false);

		Setting GetSetting(int? id = null, string name = null, bool isCache = true);

		IEnumerable<Setting> PagedList(SortingPagingBuilder sortBuider, Paging page);
	}
}