using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace App.FakeEntity.SeoGlobal
{
	public class SettingSeoGlobalViewModel
	{
		[AllowHtml]
		[Display(Name="FacebookRetargetSnippet", ResourceType=typeof(FormUI))]
		public string FacebookRetargetSnippet
		{
			get;
			set;
		}

        [AllowHtml]
        [Display(Name="FbAdminsId", ResourceType=typeof(FormUI))]
		public string FbAdminsId
		{
			get;
			set;
		}

        [AllowHtml]
        [Display(Name="FbAppId", ResourceType=typeof(FormUI))]
		public string FbAppId
		{
			get;
			set;
		}

		[AllowHtml]
		[Display(Name="GoogleRetargetSnippet", ResourceType=typeof(FormUI))]
		public string GoogleRetargetSnippet
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		[AllowHtml]
		[Display(Name="MetaTagMasterTool", ResourceType=typeof(FormUI))]
		public string MetaTagMasterTool
		{
			get;
			set;
		}

		[AllowHtml]
		[Display(Name="PublisherGooglePlus", ResourceType=typeof(FormUI))]
		public string PublisherGooglePlus
		{
			get;
			set;
		}

		[AllowHtml]
		[Display(Name="SnippetGoogleAnalytics", ResourceType=typeof(FormUI))]
		public string SnippetGoogleAnalytics
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

        [AllowHtml]
        [Display(Name = "FbLink", ResourceType = typeof(FormUI))]
        public string FbLink
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "GooglePlusLink", ResourceType = typeof(FormUI))]
        public string GooglePlusLink
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "TwitterLink", ResourceType = typeof(FormUI))]
        public string TwitterLink
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "PinterestLink", ResourceType = typeof(FormUI))]
        public string PinterestLink
        {
            get;
            set;
        }

        [AllowHtml]
        [Display(Name = "YoutubeLink", ResourceType = typeof(FormUI))]
        public string YoutubeLink
        {
            get;
            set;
        }
	}
}