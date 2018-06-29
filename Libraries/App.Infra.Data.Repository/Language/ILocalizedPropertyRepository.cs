using System.Collections.Generic;
using App.Core.Utilities;
using App.Domain.Entities.Language;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.Language
{
    public interface ILocalizedPropertyRepository : IRepositoryBase<LocalizedProperty>
	{
		LocalizedProperty GetId(int id);

		IEnumerable<LocalizedProperty> PagedList(Paging page);

		IEnumerable<LocalizedProperty> PagedSearchList(SortingPagingBuilder sortBuider, Paging page);
	}
}