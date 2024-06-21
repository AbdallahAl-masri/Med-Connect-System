using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Insurance
{
    public long InsuranceNumber { get; set; }

    public string Provider { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public string ClassType { get; set; } = null!;

    public double InPercentage { get; set; }

    public double OutPercentage { get; set; }

    public double DrPercentage { get; set; }

    public long PatientId { get; set; }
}
