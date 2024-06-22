using System;
using System.Collections.Generic;

namespace MCS.Entities;

public partial class Radiology
{
    public long Id { get; set; }

    public string? TechnicianName { get; set; }

    public string? TechnicianNotes { get; set; }

    public string ImageType { get; set; } = null!;

    public DateTime ImageDate { get; set; }

    public long? DoctorId { get; set; }

    public long PatientId { get; set; }

    public string? ImagePath { get; set; }

    public string? PatientName { get; set; }

    public long? StaffId { get; set; }
}
