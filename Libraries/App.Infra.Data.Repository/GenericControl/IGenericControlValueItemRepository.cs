using App.Domain.Entities.GenericControl;
using App.Domain.Interfaces.Repository;

namespace App.Infra.Data.Repository.GenericControl
{
	public interface IGenericControlValueItemRepository : IRepositoryBase<GenericControlValueItem>
	{
        GenericControlValueItem GetById(int id);
	}
}