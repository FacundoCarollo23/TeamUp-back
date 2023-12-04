using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserLastname { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int TrainingLevel { get; set; }

    public string? Alias { get; set; }

    public virtual ICollection<EventsComment> EventsComments { get; set; } = new List<EventsComment>();

    public virtual ICollection<RatesEvent> RatesEvents { get; set; } = new List<RatesEvent>();

    public virtual DifficultyLevel TrainingLevelNavigation { get; set; } = null!;

    public virtual ICollection<UsersActivity> UsersActivities { get; set; } = new List<UsersActivity>();

    public virtual ICollection<UsersChallenge> UsersChallenges { get; set; } = new List<UsersChallenge>();

    public virtual ICollection<UsersEvent> UsersEvents { get; set; } = new List<UsersEvent>();
}
