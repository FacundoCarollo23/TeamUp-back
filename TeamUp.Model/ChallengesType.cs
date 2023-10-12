using System;
using System.Collections.Generic;

namespace TeamUp.Model;

public partial class ChallengesType
{
    public string ChallengeTypeId { get; set; } = null!;

    public string ChallengeTypeDescription { get; set; } = null!;

    public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
}
