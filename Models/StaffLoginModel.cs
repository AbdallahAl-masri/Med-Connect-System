﻿using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class StaffLoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public int DepartmentID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public int StaffID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Password { get; set; }
        public StaffLoginModel() { }

    }
}
