using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ToDoTask
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public bool IsDraft { get; set; }
  
        public int Status { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Descreption")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        //[Required(ErrorMessage = "Room number is required")]
        //[Display(Name = "Room Number")]
        //public int RoomNo { get; set; }
        //[Required(ErrorMessage = "Floor number is required")]
        //[Display(Name = "Floor Number")]
        //public int FloorNo { get; set; }
        //[ForeignKey("Buliding")]
        //[Required(ErrorMessage = "Building  is required")]
        //[Display(Name = "Building ")]
        //public int BulidingID { get; set; }
        //[Display(Name = "Branch ")]
        //public int BranchID { get; set; }
        //[ForeignKey("ServiceType")]
        //[Required(ErrorMessage = "Service Type is required")]
        //[Display(Name = " Service Type")]
        //public int ServiceTypeID { get; set; }
        //public string ServiceType { get; set; }
        //public virtual ICollection<Reply> Replies { get; set; }
   
        //[ForeignKey("User")]
        //public int ResponsibleMaintenanceManager { get; set; }
        //[ForeignKey("User")]
       
        //public int ResponsibleMaintenanceWorker { get; set; }
        //[ForeignKey("User")]
        //public int ResponsibleBuildingManager { get; set; }
        public bool IsColesed { get; set; }
    }
   
}
