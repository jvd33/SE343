using System;
using System.Collections.Generic;
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
        public int Category { get; set; }
        public string Title { get; set; }
        public string WearableNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderNumber { get; set; }
        public TicketStatus Status { get; set; }
        public User CreatedBy { get; set; }
        public User AssignedTo { get; set; }
        public User LastUpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int Priority { get; set; }
    }
}