using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Appointment
{
    public long Id { get; set; }

    public long DepartmentId { get; set; }

    public long PatientId { get; set; }

    public string Timeslot { get; set; }

    public string? Status { get; set; }

    public long? DoctorId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;
}
