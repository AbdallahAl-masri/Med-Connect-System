namespace MCS.Models
{
    public class RegisterModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string OfficialID { get; set; }

        public RegisterModel()
        {

        }
    }
}
