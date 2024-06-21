using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class TestResultModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string Result { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string TestType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateTime TestDate { get; set; }

        public string? Technician { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string PatientName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public long PatientID { get; set; }


    }
}
