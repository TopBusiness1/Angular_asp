using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UserWebAPI.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public byte[] PasswordSalt { get; set; }
    }
}
