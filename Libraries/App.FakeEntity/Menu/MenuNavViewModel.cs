using System.Collections.Generic;
using App.Service.Language;

namespace App.FakeEntity.Menu
{
    public class MenuNavViewModel : ILocalizedModel<MenuNavViewModelLocales>
    {
        public List<MenuNavViewModel> ChildNavMenu
        {
            get;
            set;
        }

        public string CurrentVirtualId
        {
            get;
            set;
        }

        public string ImageSmallSize
        {
            get;
            set;
        }

        public string ImageMediumSize
        {
            get;
            set;
        }

        public string ImageBigSize
        {
            get;
            set;
        }

        public int MenuId
        {
            get;
            set;
        }

        public string MenuName
        {
            get;
            set;
        }

        public int OrderDisplay
        {
            get;
            set;
        }

        public string OtherLink
        {
            get;
            set;
        }

        public int? ParentId
        {
            get;
            set;
        }

        public string SeoUrl
        {
            get;
            set;
        }

        public int TemplateType
        {
            get;
            set;
        }

        public string VirtualId
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public string ColorHex
        {
            get;
            set;
        }

        public IList<MenuNavViewModelLocales> Locales { get; set; }

        public MenuNavViewModel()
        {
            Locales = new List<MenuNavViewModelLocales>();
        }
    }

    public class MenuNavViewModelLocales : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        public int LocalesId { get; set; }

        public List<MenuNavViewModel> ChildNavMenu
        {
            get;
            set;
        }

        public string CurrentVirtualId
        {
            get;
            set;
        }

        public string IconBar
        {
            get;
            set;
        }

        public string IconNav
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public int MenuId
        {
            get;
            set;
        }

        public string MenuName
        {
            get;
            set;
        }

        public int OrderDisplay
        {
            get;
            set;
        }

        public string OtherLink
        {
            get;
            set;
        }

        public int? ParentId
        {
            get;
            set;
        }

        public string SeoUrl
        {
            get;
            set;
        }

        public int TemplateType
        {
            get;
            set;
        }

        public string VirtualId
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public string ColorHex
        {
            get;
            set;
        }
    }
}