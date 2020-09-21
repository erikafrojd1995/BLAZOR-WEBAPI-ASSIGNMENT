using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Det måste vara en giltig epost-adress")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "lösenordet måste minst vara 8 tecken")]
        public string Password { get; set; }
    }
}