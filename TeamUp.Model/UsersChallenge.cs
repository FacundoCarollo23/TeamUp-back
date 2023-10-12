using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class UsersChallenge
{
    public int UserChallengeId { get; set; }

    public int ChallengeId { get; set; }

    public int UserId { get; set; }

    public virtual Challenge Challenge { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
