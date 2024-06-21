namespace MCS.Models
{
    public class ReserveAppointmentViewModel
    {
        public long PatientId { get; set; }
        public long DepartmentId { get; set; }
        public long DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}
