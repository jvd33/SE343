using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KennUTicket.Models
{
    public class TicketEvent
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public DateTime Time { get; set; }
        public Ticket Ticket { get; set; }
    }
}