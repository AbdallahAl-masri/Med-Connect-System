using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Doctor
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Speciality { get; set; } = null!;

    public string Location { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public bool Resident { get; set; }

    public long ClinicId { get; set; }

    public long StaffId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<DeptStaff> DeptStaffs { get; set; } = new List<DeptStaff>();
}
