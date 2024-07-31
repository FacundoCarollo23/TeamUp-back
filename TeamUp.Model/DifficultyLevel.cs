using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class DifficultyLevel
{
    public int DifficultyLevelId { get; set; }

    public string DifficultyName { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
