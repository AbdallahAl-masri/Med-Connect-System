using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Inpatient
{
    public long Id { get; set; }

    public long DoctorId { get; set; }

    public long DeptId { get; set; }

    public long RoomNumber { get; set; }

    public string AdmissionReason { get; set; } = null!;
}
