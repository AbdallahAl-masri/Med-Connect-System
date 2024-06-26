using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace MCS.Entities;

public partial class AspNetRole : IdentityRole<long>
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }
}
