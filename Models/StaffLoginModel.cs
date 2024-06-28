using System.ComponentModel.DataAnnotations;

namespace MCS.Models
{
    public class StaffLoginModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string StaffID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        public string Password { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "This field is Required")]
        //public string UserName { get; set; }
        public StaffLoginModel() { }

    }
}
