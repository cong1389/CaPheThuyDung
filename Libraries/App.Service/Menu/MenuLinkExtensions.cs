using System.Web.Mvc;
using App.Domain.Entities.Menu;
using App.Service.GenericControl;

namespace App.Service.Menu
{
    public static class MenuLinkExtensions
    {
        public static bool SelectedMenu(this MenuLink menuLink, int id)
        {
            var result = false;
            try
            {
                if (id == 0)
                {
                }

                var genericControlService = DependencyResolver.Current.GetService<IGenericControlService>();

                var genericControl = genericControlService.GetById(id);
                if (genericControl == null)
                {
                    return false;
                }

                foreach (var mnu in genericControl.MenuLinks)
                {
                    if (menuLink.Id == mnu.Id)
                    {
                        result = true;
                    }
                }

            }
            catch 
            {
                return false;
               
            }

            return result;
        }
    }
}
