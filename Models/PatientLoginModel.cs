namespace MCS.Models
{
    public class PatientLoginModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public PatientLoginModel()
        {

        }
    }
}
