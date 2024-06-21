using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Diagnosis
{
    public long DoctorId { get; set; }

    public long PatientId { get; set; }

    public DateTime DiagnosisDate { get; set; }

    public string Diagnosis1 { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public long Id { get; set; }
}
