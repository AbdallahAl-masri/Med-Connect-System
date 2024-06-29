using System;
using System.Collections.Generic;

namespace MCS.Models
{
    public class AppointmentViewModel
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public string Department { get; set; }
        public string AppointmentTime { get; set; }
        public string Status { get; set; }
        public string Doctor { get; set; }
    }
}
