using System.ComponentModel.DataAnnotations;

namespace App.FakeEntity.User
{
	public class ChangePasswordViewModel
	{
		[Compare("NewPassword", ErrorMessage="Mật khẩu xác nhận không chính xác.")]
		[DataType(DataType.Password)]
		[Display(Name="Xác nhận mật khẩu test")]
		public string ConfirmPassword
		{
			get;
			set;
		}

		[DataType(DataType.Password)]
		[Display(Name="Mật khẩu mới")]
		public string NewPassword
		{
			get;
			set;
		}

		[DataType(DataType.Password)]
		[Display(Name="Mật khẩu cũ")]
		public string OldPassword
		{
			get;
			set;
		}
	}
}