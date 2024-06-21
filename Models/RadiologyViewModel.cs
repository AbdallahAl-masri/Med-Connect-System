using System;
using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class RadiologyViewModel
    {
        [Display(Name = "Technician Name")]
        public string? TechnicianName { get; set; }

        [Display(Name = "Technician Notes")]
        public string? TechnicianNotes { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Image Type")]
        public string ImageType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Image Date")]
        [DataType(DataType.Date)]
        public DateTime ImageDate { get; set; }

        [Display(Name = "Doctor ID")]
        public long? DoctorId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Patient ID")]
        public long PatientId { get; set; }

        [Display(Name = "Image Path")]
        public string? ImagePath { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
    }
}

