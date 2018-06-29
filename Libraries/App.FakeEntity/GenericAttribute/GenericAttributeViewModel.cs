using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.GenericAttribute
{
    public class GenericAttributeViewModel
    {
        [Display(Name = "Entity", ResourceType = typeof(FormUI))]
        public int EntityId
        {
            get;
            set;
        }

        [Display(Name = "Entity", ResourceType = typeof(FormUI))]
        public string KeyGroup
        {
            get;
            set;
        }

        [Display(Name = "Entity", ResourceType = typeof(FormUI))]
        public string Key
        {
            get;
            set;
        }

        [Display(Name = "Entity", ResourceType = typeof(FormUI))]
        public int Value
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Entity", ResourceType = typeof(FormUI))]
        public int StoreId
        {
            get;
            set;
        }
    }
}
