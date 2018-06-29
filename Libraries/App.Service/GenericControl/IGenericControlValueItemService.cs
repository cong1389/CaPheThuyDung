using System.Collections.Generic;
using App.Domain.Entities.GenericControl;
using App.Domain.Interfaces.Services;

namespace App.Service.GenericControl
{
    public interface IGenericControlValueItemService : IBaseService<GenericControlValueItem>
	{
		GenericControlValueItem GetById(int id, bool isCache = true);

        IEnumerable<GenericControlValueItem> GetByOption(int? genericControlValueId = null
           , int? entity = null
           , int status = 1, bool isCache = true);

    }
}