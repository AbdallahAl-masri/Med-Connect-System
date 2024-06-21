using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class DeptStaff
{
    public long StaffId { get; set; }

    public string Name { get; set; } = null!;

    public long DepartmentId { get; set; }

    public string Role { get; set; } = null!;

    public long? DoctorId { get; set; }
}
