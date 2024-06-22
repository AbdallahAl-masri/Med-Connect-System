using MCS.Areas.Identity;
namespace MCS.Models
{
    public class Patient 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public long PhoneNumber { get; set; }
        public string InsuranceCompany { get; set; }
        public long InsuranceNumber { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string OfficialID { get; set; }
        public string PasswordHash { get; set; }

        public Patient()
        {
            //this.Age = this.BirthDate - DateTime.Now.ToShortDateString();
        }
    }
}
