using System.ComponentModel.DataAnnotations;

namespace BTLG06WNC;

public class ResourceFileView
{
    public int IResourceFileId { get; set; }
    [Required]
    public IFormFile? SFilename { get; set; }
    [Required(ErrorMessage = "Ngày sai định dạng")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime? DUploaddate { get; set; }
    [Required]
    public string? SDescription { get; set; }
    public string? SFileName { get; set; }

    public int ICategoryId { get; set; }

    public List<FileCategory>? LFileCategories  { get; set; }
}
