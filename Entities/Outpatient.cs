using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Outpatient
{
    public long Id { get; set; }

    public DateTime CheckBackDate { get; set; }
}
