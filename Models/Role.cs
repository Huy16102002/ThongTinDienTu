using System;
using System.Collections.Generic;

namespace BTLG06WNC;

public partial class Role
{
    public int IRoleId { get; set; }

    public string? SRolename { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
