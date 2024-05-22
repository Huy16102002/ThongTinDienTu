using System.ComponentModel.DataAnnotations;

namespace BTLG06WNC;

public class LoginView
{
    [Required(ErrorMessage = "Nhập tên email")]
    [DataType(DataType.EmailAddress)]
    public string? SEmail { get; set; }
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Nhập mật khẩu")]
    public string? SPassword { get; set; }
}
