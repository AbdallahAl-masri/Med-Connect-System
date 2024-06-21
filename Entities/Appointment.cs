using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Appointment
{
    public long Id { get; set; }

    public long ClinicId { get; set; }

    public long PatientId { get; set; }

    public DateTime Timeslot { get; set; }

    public string Status { get; set; } = null!;
}
