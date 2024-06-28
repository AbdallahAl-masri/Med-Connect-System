using MCS.Entities;
namespace MCS.Models
{
    public class ReserveAppointmentViewModel
    {
        public long PatientId { get; set; }
        public string Department { get; set; }
        public long DoctorId { get; set; }
        public string AppointmentTime { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
