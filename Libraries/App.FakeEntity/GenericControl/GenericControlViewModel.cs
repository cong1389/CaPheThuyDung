using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Service.Language;
using Resources;

namespace App.FakeEntity.GenericControl
{
    public class GenericControlViewModel : ILocalizedModel<GenericControlLocalesViewModel>
    {
        [Display(Name = "Name", ResourceType = typeof(FormUI))]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "Description", ResourceType = typeof(FormUI))]
        public string Description
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
        public int? OrderDisplay
        {
            get;
            set;
        }

        [Display(Name = "Status", ResourceType = typeof(FormUI))]
        public int Status
        {
            get;
            set;
        }

        [Display(Name = "MenuName", ResourceType = typeof(FormUI))]
        public int MenuId
        {
            get;
            set;
        }

        [Display(Name = "ControlType", ResourceType = typeof(FormUI))]
        public int ControlTypeId { get; set; }
        
        public virtual ICollection<GenericControlValueViewModel> GenericControlValues
        {
            get;
            set;
        }

        public IList<GenericControlLocalesViewModel> Locales { get; set; }

        //public virtual List<MenuLinkViewModel> MenuLinks { get; set; }

        public GenericControlViewModel()
        {
            Locales = new List<GenericControlLocalesViewModel>();
        }
    }

    public class GenericControlLocalesViewModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        public int LocalesId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(FormUI))]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "Description", ResourceType = typeof(FormUI))]
        public string Description
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "OrderDisplay", ResourceType = typeof(FormUI))]
        public int? OrderDisplay
        {
            get;
            set;
        }

        [Display(Name = "Status", ResourceType = typeof(FormUI))]
        public int Status
        {
            get;
            set;
        }


        [Display(Name = "ControlType", ResourceType = typeof(FormUI))]
        public int ControlTypeId { get; set; }


        [Display(Name = "Entity", ResourceType = typeof(FormUI))]
        public int MenuId
        {
            get;
            set;
        }

        //public List<MenuLinkViewModel> MenuLinks
        //{
        //    get;
        //    set;
        //}

        public virtual ICollection<GenericControlValueViewModel> GenericControlValues
        {
            get;
            set;
        }

       
    }
}