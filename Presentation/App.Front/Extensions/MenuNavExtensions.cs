using System.Collections.Generic;
using System.Linq;
using App.FakeEntity.Menu;

namespace App.Front.Extensions
{
    public static class MenuNavExtensions
    {
        public static List<MenuNavViewModel> MenuNavsViewModels(int? parentId, IEnumerable<MenuNavViewModel> source)
        {
            var navViewModels = (from x in source
                                 orderby x.OrderDisplay descending
                                 select x).Where(x =>
                                 {
                                     var nullable1 = x.ParentId;
                                     var nullable = parentId;
                                     if (nullable1.GetValueOrDefault() != nullable.GetValueOrDefault())
                                     {
                                         return false;
                                     }
                                     return nullable1.HasValue == nullable.HasValue;
                                 }).Select(x => new MenuNavViewModel
                                 {
                                     MenuId = x.MenuId,
                                     ParentId = x.ParentId,
                                     MenuName = x.MenuName,
                                     SeoUrl = x.SeoUrl,
                                     OrderDisplay = x.OrderDisplay,
                                     ImageBigSize = x.ImageBigSize,
                                     CurrentVirtualId = x.CurrentVirtualId,
                                     VirtualId = x.VirtualId,
                                     TemplateType = x.TemplateType,
                                     OtherLink = x.OtherLink,
                                     ImageMediumSize = x.ImageMediumSize,
                                     ImageSmallSize = x.ImageSmallSize,
                                     ChildNavMenu = MenuNavsViewModels(x.MenuId, source)
                                 }).OrderBy(x => x.OrderDisplay).ToList();

            return navViewModels;
        }
    }
}