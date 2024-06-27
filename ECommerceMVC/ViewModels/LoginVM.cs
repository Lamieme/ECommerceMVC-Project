using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.ViewModels
{
	public class LoginVM
	{
		//Hien thi thong tin
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage = "Vui lòng nhập tên")]
		[MaxLength(20, ErrorMessage ="Tối đa 20 ký tự")]
		public string UserName { get; set; }

		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Vui lòng nhập mật khẩu đăng nhập")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
