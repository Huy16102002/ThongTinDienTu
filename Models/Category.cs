using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLG06WNC;

public partial class Category
{
    [Key]
    public int ICategoryId { get; set; }
    [Required]
    public string? STitle { get; set; }

    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
}
