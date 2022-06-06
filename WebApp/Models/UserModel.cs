using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UserModel
    {

        [Required(ErrorMessage = "Your email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Adress")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Your password is required")]
        //[MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }
    }
}
