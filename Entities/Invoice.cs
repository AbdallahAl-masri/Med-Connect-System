using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Invoice
{
    public long Id { get; set; }

    public long PatientId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public double Total { get; set; }

    public string Notes { get; set; } = null!;

    public bool Status { get; set; }
}
