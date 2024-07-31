using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
