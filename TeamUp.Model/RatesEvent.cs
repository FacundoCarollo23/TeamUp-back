using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class RatesEvent
{
    public int RateEventId { get; set; }

    public int? EventId { get; set; }

    public int? UserId { get; set; }

    public int? RateId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Rate? Rate { get; set; }

    public virtual User? User { get; set; }
}
