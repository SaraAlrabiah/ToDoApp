
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ViewTask
    {
        [DisplayName("ID")]
        public int ID { get; set; }
        public List<IFormFile> Attachments { get; set; }
        [DisplayName("Building Name")]
        public string BuildingName { get; set; }
        public int ResponsibleMaintenanceWorker { get; set; }
        public int UserID { get; set; }
        [DisplayName("Decsription")]
        public string Description { get; set; }
        [DisplayName("Request Date")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [DisplayName("Response Date")]
        [DataType(DataType.Date)]
        public DateTime ResponseDate { get; set; }
        [DisplayName("Room No")]
        public int RoomNo { get; set; }
        [DisplayName("Floor No")]
        public int FloorNo { get; set; }
        public int BranchID { get; set; }
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        [DisplayName("Building ID")]
        public int BulidingID { get; set; }
        [DisplayName("Maintenance Type")]
        public string ServiceType { get; set; }
        public int ServiceTypeID { get; set; }
        [DisplayName("Status")]
        public int Status { get; set; }
        [DisplayName("Client")]
        public string ClientFullName { get; set; }
        [DisplayName("Maintenance Worker ")]
        public string WorkerFullName { get; set; }

    }

    public class WorkerInfo {

        public int ID { get; set; }

        [DisplayName("Assign To")]
        public string FirstName{get;set; }
        public string LastName { get; set; }

    }


}
