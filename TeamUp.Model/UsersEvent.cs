using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class UsersEvent
{
    public int UserEventId { get; set; }

    public bool RolId { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
