using System;
using System.Collections.Generic;

namespace BTLG06WNC;

public partial class Content
{
    public int IContentId { get; set; }

    public string? STitle { get; set; }

    public DateTime? DCreatedate { get; set; }

    public string? SMainbody { get; set; }

    public string? SSource { get; set; }

    public int? ICategoryId { get; set; }

    public string? SImage { get; set; }

    public virtual Category? ICategory { get; set; }



}
