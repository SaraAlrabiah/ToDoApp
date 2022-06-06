using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Role
    {
        [Key]

        public int ID { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }


    }
}
