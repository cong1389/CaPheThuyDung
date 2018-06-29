using App.Core.Common;

namespace App.Domain.Entities.GlobalSetting
{
	public class SettingSeoGlobal : AuditableEntity<int>
	{
		public string FacebookRetargetSnippet
		{
			get;
			set;
		}

		public string FbAdminsId
		{
			get;
			set;
		}
		
		public string FbAppId
		{
			get;
			set;
		}

		public string GoogleRetargetSnippet
		{
			get;
			set;
		}

		public string MetaTagMasterTool
		{
			get;
			set;
		}

		public string PublisherGooglePlus
		{
			get;
			set;
		}

		public string SnippetGoogleAnalytics
		{
			get;
			set;
		}

		public int Status
		{
			get;
			set;
		}

        public string FbLink
        {
            get;
            set;
        }

        public string GooglePlusLink
        {
            get;
            set;
        }

        public string TwitterLink
        {
            get;
            set;
        }
        public string PinterestLink
        {
            get;
            set;
        }
        public string YoutubeLink
        {
            get;
            set;
        }

        public SettingSeoGlobal()
		{
		}
	}
}