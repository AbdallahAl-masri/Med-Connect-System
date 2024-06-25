using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MCS.Entities;

public partial class AspNetUserLogin : IdentityUserLogin<long>
{
    public string? LoginProvider { get; set; }

    public string? ProviderKey { get; set; }

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;
}
