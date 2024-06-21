using System;
using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class TestViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Patient ID")]
        public long PatientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Test Type")]
        public string TestType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [Display(Name = "Test Date")]
        [DataType(DataType.Date)]
        public DateTime TestDate { get; set; }

    }
}
