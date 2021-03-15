using System.ComponentModel.DataAnnotations.Schema;
using Users.Models.Data;

namespace Users.Models.Dao
{
    [Table("Users")]
    public class UserDao
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Salt { get; set; }
        public Role Role { get; set; }
    }
}