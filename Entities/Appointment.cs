using System;
using System.Collections.Generic;
using MCS.Models;
namespace MCS.Entities;

public partial class Appointment
{
    public long Id { get; set; }

    public long DepartmentId { get; set; }

    public long PatientId { get; set; }
    public int? DoctorId { get; set; }

    //could be changed into DateTime
    public String Timeslot { get; set; }

    public string Status { get; set; } = null!;
    public Appointment()
    {
        
    }
}
