using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class Activity
{
    public int ActivityId { get; set; }

    public string ActivityName { get; set; } = null!;

    public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<UsersActivity> UsersActivities { get; set; } = new List<UsersActivity>();
}
