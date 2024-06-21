using System;
using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class TestReportModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public long PatientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string PatientName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string TestName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public string ReportContent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        public DateTime ReportDate { get; set; }
    }
}
