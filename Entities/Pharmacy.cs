using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Pharmacy
{
    public long MedicineId { get; set; }

    public long Stock { get; set; }

    public long Shelf { get; set; }

    public string MedicineName { get; set; } = null!;
}
