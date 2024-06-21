using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class PatientVisit
{
    public long VisitId { get; set; }

    public long PatientId { get; set; }

    public DateTime VisitDate { get; set; }
}
