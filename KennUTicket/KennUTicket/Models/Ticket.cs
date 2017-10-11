using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KennUTicket.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string TicketType { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Priority { get; set; }
    }
}