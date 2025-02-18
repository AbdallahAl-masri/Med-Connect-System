﻿using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Department
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public long BedCount { get; set; } = 0;

    public long DeptHeadId { get; set; } = 0;

    public string WorkingHours { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
