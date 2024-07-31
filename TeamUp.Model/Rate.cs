using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class Rate
{
    public int RateId { get; set; }

    public int RateValue { get; set; }

    public string RateDescription { get; set; } = null!;

    public virtual ICollection<RatesEvent> RatesEvents { get; set; } = new List<RatesEvent>();
}
