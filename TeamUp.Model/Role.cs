using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class Role
{
    public bool RolId { get; set; }

    public string RolDescription { get; set; } = null!;

    public virtual ICollection<UsersEvent> UsersEvents { get; set; } = new List<UsersEvent>();
}
