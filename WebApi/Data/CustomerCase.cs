using System;
using System.Collections.Generic;

namespace WebApi.Data
{
    public partial class CustomerCase
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
}
