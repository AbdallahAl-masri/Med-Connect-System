using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class AspNetUserToken
{
    public string UserId { get; set; } = null!;

    public string? LoginProvider { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }
}
