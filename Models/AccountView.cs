using System.ComponentModel.DataAnnotations;

namespace BTLG06WNC;

public class AccountView
{
    [Key]
    public int IAccountId { get; set; }
    [Required(ErrorMessage = "Nhập tên tài khoản")]
    public string? SName { get; set; }
    [Required(ErrorMessage = "Nhập email")]
    [DataType(DataType.EmailAddress)]
    public string? SEmail { get; set; }
    [Required(ErrorMessage = "Nhập mật khẩu")]
    public string? SPassword { get; set; }
    [Required(ErrorMessage = "Nhập sô điện thoại")]
    public string? SPhone { get; set; }

    public IFormFile? Image { get; set; }

    public string? SAvatar { get; set; }
    [Required(ErrorMessage = "Chọn ngày sinh")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime? DBirthofdate { get; set; }

    public int? IRoleId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

}
