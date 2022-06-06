using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        // Email is the username
        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = " Email")]
    
        public string Username { get; set; }
        [Required(ErrorMessage = "Your phoneNumber is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = " phoneNumber")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public int Status { get; set; }
        public int ServicesCount { get; set; }
        public string Password { get; set; }
       
        public int loginStatus { get; set; }
        public virtual ICollection<AuditTrail> AuditTrails { get; set; }
        public virtual ICollection<ToDoTask> ToDoTasks { get; set; }

    }
}
