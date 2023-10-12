using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class UsersActivity
{
    public int UserActivityId { get; set; }

    public int ActivityId { get; set; }

    public int UserId { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
