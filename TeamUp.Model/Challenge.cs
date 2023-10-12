using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class Challenge
{
    public int ChallengeId { get; set; }

    public string ChallengeTypeId { get; set; } = null!;

    public int ActivityId { get; set; }

    public string ChallengeName { get; set; } = null!;

    public string? ChallengeDescription { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual ChallengesType ChallengeType { get; set; } = null!;

    public virtual ICollection<UsersChallenge> UsersChallenges { get; set; } = new List<UsersChallenge>();
}
