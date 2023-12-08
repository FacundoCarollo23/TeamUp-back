using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class Event
{
    public int EventId { get; set; }

    public int ActivityId { get; set; }

    public int DifficultyLevelId { get; set; }

    public int? CountryId { get; set; }

    public string? EventName { get; set; }

    public string? EventDescription { get; set; }

    public string? City { get; set; }

    public DateTime DateTime { get; set; }

    public int? UserCount { get; set; }

    public DateTime EventCreateDateTime { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual Country? Country { get; set; }

    public virtual DifficultyLevel DifficultyLevel { get; set; } = null!;

    public virtual ICollection<EventsComment> EventsComments { get; set; } = new List<EventsComment>();

    public virtual ICollection<RatesEvent> RatesEvents { get; set; } = new List<RatesEvent>();

    public virtual ICollection<UsersEvent> UsersEvents { get; set; } = new List<UsersEvent>();
}
