using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class PatientAppointment
{
    public long AppointmentId { get; set; }

    public long PatientId { get; set; }

    public long ClinicId { get; set; }
}
