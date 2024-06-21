using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Prescription
{
    public long Id { get; set; }

    public long PatientId { get; set; }

    public string PatientName { get; set; } = null!;

    public string? Comments { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
