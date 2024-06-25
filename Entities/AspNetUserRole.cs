using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MCS.Entities;

public partial class AspNetUserRole : IdentityUserRole<long>
{
    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;
}
