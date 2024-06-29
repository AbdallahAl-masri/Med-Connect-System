using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MCS.Entities
{
    public partial class DeptStaff : IdentityUser<long>
    {
        public string FirstName { get; set; } = null!;
        public long DepartmentId { get; set; }
        public string Role { get; set; } = null!;
        public long? DoctorId { get; set; }
        public string LastName { get; set; } = null!;
        public  string? EmailConfirmed { get; set; }
        public long Id { get; set; }
        public override string UserName { get; set; } = null!;
        public override string? NormalizedUserName { get; set; }
        public override string? NormalizedEmail { get; set; }
        public override string? SecurityStamp { get; set; }
        public override string? ConcurrencyStamp { get; set; }
        public override string? PhoneNumber { get; set; }
        public  bool? PhoneNumberConfirmed { get; set; }
        public  bool? TwoFactorEnabled { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
        public  bool? LockoutEnabled { get; set; }
        public override int AccessFailedCount { get; set; }
        public override string? Email { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}
