namespace MCS.Models
{
    public class AppointmentRequest
    {
        public int DepartmentID { get; set; }
        public int? DoctorID { get; set; }         
        public string Appointmentperiod { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}
