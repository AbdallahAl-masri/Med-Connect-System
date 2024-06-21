using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class AspNetRoleClaim
{
    public long Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
