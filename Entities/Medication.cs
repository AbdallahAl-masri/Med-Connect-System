using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Medication
{
    public long Id { get; set; }

    public long PrescriptionId { get; set; }

    public string MedicineName { get; set; } = null!;

    public string Dosage { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;
}
