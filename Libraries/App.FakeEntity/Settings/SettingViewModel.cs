namespace App.FakeEntity.Setting
{
	public class SettingViewModel
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Value
		{
			get;
			set;
		}

		public int StoreId { get; set; }

		public SettingViewModel()
		{
		}
	}
}