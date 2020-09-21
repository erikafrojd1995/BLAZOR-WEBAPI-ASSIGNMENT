using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class User
    {

        

        public User()
        {
            CustomerCase = new HashSet<CustomerCase>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<CustomerCase> CustomerCase { get; set; }
    }
}
