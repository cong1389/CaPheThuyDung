using System.ComponentModel.DataAnnotations;
using Resources;

namespace App.FakeEntity.Language
{
	public class LocaleStringResourceViewModel
	{
        public int Id
        {
            get;
            set;
        }

        public int LanguageId
        {
			get;
			set;
		}        

		[Display(Name= "ResourceName", ResourceType=typeof(FormUI))]
		public string ResourceName
        {
			get;
			set;
		}

        [Display(Name = "ResourceValue", ResourceType = typeof(FormUI))]
        public string ResourceValue
        {
            get;
            set;
        }
       
        public bool IsFromPlugin
        {
            get;
            set;
        }

        public bool IsTouched
        {
            get;
            set;
        }
	}
}