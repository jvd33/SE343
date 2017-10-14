using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KennUTicket.Models
{
    public class Ticket
    {
        //could be a GUID, int for now
        public int ID { get; set; }

        //desc = reasons for calling
        public string Description { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string WearableNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderNumber { get; set; }

        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public TicketStatus Status { get; set; }

        public string CreatedByID { get; set; }
        [ForeignKey("CreatedByID")]
        public User CreatedBy { get; set; }

        public string AssignedToID { get; set; }
        [ForeignKey("AssignedToID")]
        public User AssignedTo { get; set; }

        public string LastUpdatedByID { get; set; }
        [ForeignKey("LastUpdatedByID")]
        public  User LastUpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Priority { get; set; }
    }
}