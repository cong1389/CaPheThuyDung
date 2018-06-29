using Domain.Entities.Customers;

namespace App.Service.Common
{
    public interface IWorkContext 
    {
        Customer CurrentCustomer { get; set; }

        Domain.Entities.Language.Language WorkingLanguage { get; set; }
    }
}