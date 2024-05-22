using System;
using System.Collections.Generic;

namespace BTLG06WNC;

public partial class Feedback
{
    public int IFeedbackId { get; set; }

    public string? SContent { get; set; }

    public int? IAccountId { get; set; }

    public DateTime? IFeedbackdate { get; set; }

    public string? SResponse { get; set; }

    public virtual Account? IAccount { get; set; }
}
