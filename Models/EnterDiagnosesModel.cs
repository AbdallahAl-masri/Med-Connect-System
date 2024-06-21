using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class EnterDiagnosesModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public long PatientID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string PatientName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        [DataType(DataType.Date)]
        public DateTime DiagnoseDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Diagnosed { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public long DoctorID { get; set; }
    }
}
