namespace MCS.Models
{
    public class ReserveAppointmentViewModel
    {
        public long PatientId { get; set; }
        public string Department { get; set; }
        public long DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}
