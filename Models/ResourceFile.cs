using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLG06WNC;

public partial class ResourceFile
{
    public int IResourceFileId { get; set; }

    public string? SFilename { get; set; }
    
    public DateTime? DUploaddate { get; set; }
    [Required]
    public string? SDescription { get; set; }

    public int ICategoryId { get; set; }

    public virtual FileCategory? ICategory { get; set; }
}
