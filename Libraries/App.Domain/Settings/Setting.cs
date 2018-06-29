using App.Core.Common;

namespace App.Domain.Entities.Setting
{
	public class Setting : AuditableEntity<int>
	{
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

		public Setting()
		{
		}
	}
}