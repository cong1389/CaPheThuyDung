using System.Web.Mvc;
using Domain.Entities.Customers;

namespace App.Service.GenericAttribute
{
    public static class GenericAttributeExtentions
    {
        public static string GetAttribute(this Customer entity, string keyGroup, string key, IGenericAttributeService genericAttributeService, int storeId = 0)
        {
            if (genericAttributeService == null)
            {
                genericAttributeService = DependencyResolver.Current.GetService<IGenericAttributeService>();
            }

            var prop = genericAttributeService.GetByKey(entity.Id, keyGroup, key);
            return prop?.Value;
        }
    }
}
