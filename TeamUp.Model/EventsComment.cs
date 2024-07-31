using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class EventsComment
{
    public int EventCommentId { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public string? Comment { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
