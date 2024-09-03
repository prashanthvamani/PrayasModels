using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrayasModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName field is required.")]
        [Display(Name = "UserName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public string EMPID { get; set; }

    }
}
