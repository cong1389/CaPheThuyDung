using System.Linq;
using App.Domain.Entities.GenericControl;
using App.Infra.Data.Common;
using App.Infra.Data.DbFactory;

namespace App.Infra.Data.Repository.GenericControl
{
    public class GenericControlValueItemRepository : RepositoryBase<GenericControlValueItem>, IGenericControlValueItemRepository
	{
		public GenericControlValueItemRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		protected override IOrderedQueryable<GenericControlValueItem> GetDefaultOrder(IQueryable<GenericControlValueItem> query)
		{
			var genericControlValueItem = 
				from p in query
				orderby p.Id
				select p;

			return genericControlValueItem;
		}

		public GenericControlValueItem GetById(int id)
		{
			var genericControlValueItem = FindBy(x => x.Id == id).FirstOrDefault();
			return genericControlValueItem;
		}
	}
}