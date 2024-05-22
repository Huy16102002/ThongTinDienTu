using System.ComponentModel.DataAnnotations;

namespace BTLG06WNC;

public class RegisterView
{
    [Required(ErrorMessage = "Đặt tên tài khoản")]
    public string? SAccountName { get; set; }
    [Required(ErrorMessage = "Nhập tên email")]
    [DataType(DataType.EmailAddress)]
    public string? SEmail { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Đặt mật khẩu")]
    public string? SPassword { get; set; }
    [Required(ErrorMessage = "Xác nhận mật khẩu")]
    public string? SAuthenPassword { get; set; }
    [Required(ErrorMessage = "Chọn ngày sinh")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime? DBirthofdate { get; set; }
}
