using Models.Users.Models.Data;

namespace Models.Users.Models.Dto
{
    public class UserDataRow
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
    }
}