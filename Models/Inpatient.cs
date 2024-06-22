namespace MCS.Models
{
    public class Inpatient
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }
        public int DeptID { get; set; }
        public int RoomNumber { get; set; }
        public string AdmissionReason { get; set; }
        public Inpatient() { 
            ID = 0;
            DoctorID = 0;
            DeptID = 0;
            RoomNumber = 0;
            AdmissionReason = "";   
        }

    }
}
