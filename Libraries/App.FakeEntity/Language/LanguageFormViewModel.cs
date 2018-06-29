using System.ComponentModel.DataAnnotations;
using System.Web;
using Resources;

namespace App.FakeEntity.Language
{
    public class LanguageFormViewModel
	{
        public int Id
        {
            get;
            set;
        }

        [Display(Name= "LanguageCode", ResourceType=typeof(FormUI))]
		public string LanguageCode
        {
			get;
			set;
		}

        [Display(Name = "LanguageName", ResourceType = typeof(FormUI))]
        public string LanguageName
        {
            get;
            set;
        }

        [Display(Name="Image", ResourceType=typeof(FormUI))]
		public HttpPostedFileBase File
		{
			get;
			set;
		}

        [Display(Name = "Image", ResourceType = typeof(FormUI))]
        public string Flag
        {
            get;
            set;
        }

        [Display(Name="Status", ResourceType=typeof(FormUI))]
		public int Status
		{
			get;
			set;
		}
	}
}