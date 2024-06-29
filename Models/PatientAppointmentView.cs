namespace MCS.Models
{
    public class PatientAppointmentView
    {
        public string Department { get; set; }
        public string? Doctor { get; set; }
        public string Timeslot { get; set; }
        public DateTime Datetime { get; set; }
        public PatientAppointmentView()
        {

        }

    }
}
