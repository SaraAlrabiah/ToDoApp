
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SignUpModel
    {

        [Required(ErrorMessage = "Your email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Adress")]
        public string UserName { get; set; }
       
        [Required(ErrorMessage = "Your phone number is required")]
        [DataType(DataType.Text)]
        [Display(Name = "phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Your password is required")]
        [MinLength(7, ErrorMessage = "Your password must be longer than 6")]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Your confirmation password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        [Display(Name = "password confirmation")]
        public string Passwordconfirmation { get; set; }
    }
}
