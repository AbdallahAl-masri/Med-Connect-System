using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class PatientRadiology
{
    public long PatientId { get; set; }

    public long ImageId { get; set; }
}
