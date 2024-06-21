using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Test
{
    public long Id { get; set; }

    public string? NormalRange { get; set; }

    public string? Results { get; set; }

    public string TestType { get; set; } = null!;

    public DateTime TestDate { get; set; }

    public string? Technician { get; set; }

    public bool? RecievedResult { get; set; }

    public long? DoctorId { get; set; }

    public long PatientId { get; set; }

    public string PatientName { get; set; } = null!;
}
