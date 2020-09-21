using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerCase = new HashSet<CustomerCase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CustomerCase> CustomerCase { get; set; }
    }
}
