using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MCS.Entities;

public partial class DeptStaff : IdentityUser<long>
{
    public string FirstName { get; set; } = null!;

    public long DepartmentId { get; set; }

    public string Role { get; set; } = null!;

    public long? DoctorId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailConfirmed { get; set; } = null!;

    public long Id { get; set; }

    public string UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool? LockoutEnabled { get; set; }

    public int? AccessFailedCount { get; set; }

    public string? Email { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
