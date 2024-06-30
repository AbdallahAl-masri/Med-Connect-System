namespace MCS.Models
{
    public class DeptStaffModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long DepartmentId { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long StaffId { get; set; }
        public string Speciality { get; set; } = "";
    }
}
