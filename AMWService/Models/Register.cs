using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AMWService.Models
{
    public class Register
    {
        [Required(ErrorMessage="Username is requied")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is requied")]
        public string Password { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is requied")]
        public string Email { get; set; }
        public string Department { get; set; }
    }
}
