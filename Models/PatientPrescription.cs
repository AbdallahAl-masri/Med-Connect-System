namespace MCS.Models
{
    public class PatientPrescription
    {
        public DateTime Date { get; set; }
        public List<MCS.Entities.Medication> Meds { get; set; }
        public PatientPrescription()
        {

        }

    }
}
