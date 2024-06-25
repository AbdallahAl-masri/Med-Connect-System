using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MCS.Entities;

public partial class AspNetUserToken : IdentityUserToken<long>
{
    public string UserId { get; set; } = null!;

    public string? LoginProvider { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }
}
