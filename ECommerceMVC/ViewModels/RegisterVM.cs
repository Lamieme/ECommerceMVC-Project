using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên đăng nhập")]
        //Add validation
        //Check theo db
        [Required(ErrorMessage ="*")]
        [MaxLength(20, ErrorMessage ="Tối đa 20 ký tự")]
        public string MaKh { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; } = true;

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [MaxLength(24, ErrorMessage = "Tối đa 24 ký tự")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage ="Chưa đúng định dạng")]
        [Display(Name = "Số điện thoại")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage ="Chưa đúng định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
