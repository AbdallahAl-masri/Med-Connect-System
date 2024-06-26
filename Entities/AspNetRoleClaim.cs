using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace MCS.Entities;

public partial class AspNetRoleClaim : IdentityRoleClaim<long>
{
    public long Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
