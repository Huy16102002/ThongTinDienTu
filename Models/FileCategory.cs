using System;
using System.Collections.Generic;

namespace BTLG06WNC;

public partial class FileCategory
{
    public int IFileCategoryId { get; set; }

    public string? STitle { get; set; }

    public virtual ICollection<ResourceFile> ResourceFiles { get; set; } = new List<ResourceFile>();
}
