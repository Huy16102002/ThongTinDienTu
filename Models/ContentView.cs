using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTLG06WNC;

public class ContentView
{
    public int IContentId { get; set; }
    [Required(ErrorMessage = "Tiêu đề không được để trống")]
    public string? STitle { get; set; }
    [Required(ErrorMessage = "Ngày sai định dạng")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime? DCreatedate { get; set; }
    [Required(ErrorMessage = "Phải có nội dung chính")]
    public string? SMainbody { get; set; }
    public string? SSource { get; set; }
    public int? ICategoryId { get; set; }
    public string? SFilename { get; set; }
    
    public IFormFile? sImage {get; set;}
    public List<Category>? categories {get; set; } = new List<Category>();  
    public List<Content>? relatedContents {get; set; } = new List<Content>();
}
