using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class PrescriptionViewModel
    {
        public PrescriptionViewModel()
        {
            Medications = new List<MedicationViewModel>();
            PatientId = 0;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public long PatientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; } = null!;

        public string? Comments { get; set; }

        public List<MedicationViewModel> Medications { get; set; }
    }

    public class MedicationViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string MedicineName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Dosage { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Duration { get; set; } = null!;
    }
}
