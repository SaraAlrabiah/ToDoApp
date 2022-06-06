using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public class AuditTrail
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        public int Actor { get; set; }
   
        public string ActionType { get; set; }
     
        public DateTime ActionDate { get; set; }
     

    }
}
