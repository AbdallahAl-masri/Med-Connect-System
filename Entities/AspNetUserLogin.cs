using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class AspNetUserLogin
{
    public string? LoginProvider { get; set; }

    public string? ProviderKey { get; set; }

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;
}
