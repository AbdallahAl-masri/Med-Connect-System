using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Patient
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public long PhoneNumber { get; set; }

    public string? InsuranceCompany { get; set; }

    public long? InsuranceNumber { get; set; }

    public long? Age { get; set; }

    public string? Email { get; set; } = null!;

    public string OfficialId { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
