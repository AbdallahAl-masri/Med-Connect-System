using MCS.Entities;
namespace MCS.Models
{
    public class PatientRecord
    {
        public MCS.Entities.Radiology[]radiology {  get; set; }
        public MCS.Entities.Test []test { get; set; }
        public MCS.Entities.Prescription []prescription { get; set; }
        public MCS.Entities.Appointment[] appointments { get; set; }
        public PatientRecord()
        {

        }


    }
}
