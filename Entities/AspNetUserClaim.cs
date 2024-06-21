using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class AspNetUserClaim
{
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
