using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace MCS.Entities;

public partial class AspNetUserClaim : IdentityUserClaim<long>
{
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
