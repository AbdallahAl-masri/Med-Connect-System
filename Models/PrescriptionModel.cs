using MCS.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class PrescriptionModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public long PatientId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string PatientName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public List<Prescription> Prescriptions { get; set; }
    }
}
