using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KennUTicket.Models
{
    public class TicketEvent
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public DateTime Time { get; set; }

        [ForeignKey("TicketID")]
        public Ticket Ticket { get; set; }
        public int TicketID { get; set; }
    }
}