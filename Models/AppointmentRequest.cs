namespace MCS.Models
{
    public class AppointmentRequest
    {
        public long DepartmentId { get; set; }
        public long? DoctorID { get; set; }   
        public string DepartmentName { get; set; }
        public string Appointmentperiod { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}
