using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class DoctorAttendance
{
    public long PatientId { get; set; }

    public long DoctorId { get; set; }
}
