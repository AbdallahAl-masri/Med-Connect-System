namespace MCS.Models
{
    public class DoctorAttendance
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DoctorAttendance(int PatientID,int DoctorID)
        {
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
        }
    }
}
