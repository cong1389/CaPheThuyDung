using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace App.FakeEntity.Language
{
	public class LocalizedPropertyViewModel
	{
        [AllowHtml]
        [Display(Name= "LocaleValue", ResourceType=typeof(FormUI))]
		public string LocaleValue
        {
			get;
			set;
		}

        [Display(Name = "LocaleValue", ResourceType = typeof(FormUI))]
        public string LocaleKey
        {
            get;
            set;
        }

        [Display(Name = "LocaleValue", ResourceType = typeof(FormUI))]
        public string LocaleKeyGroup
        {
            get;
            set;
        }

        [Display(Name = "LocaleValue", ResourceType = typeof(FormUI))]
        public int EntityId
        {
			get;
			set;
		}

        public int Id
        {
            get;
            set;
        }

        [Display(Name= "LocaleValue", ResourceType=typeof(FormUI))]
		public int LanguageId
        {
			get;
			set;
		}
                
        public LanguageFormViewModel Language
        {
            get;
            set;
        }
	}
}