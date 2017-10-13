using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KennUTicket.DAL;
using KennUTicket.Models;

namespace KennUTicket.Extensions
{
    public static class TicketExtensions
    {
        public static List<String> GetUsers(this Ticket ticket)
        {
            List<String> users = new List<String>();
            using (var db = new TicketContext())
            {
                foreach (User u in db.Users)
                {
                    users.Add(u.Username);
                }
            }
            return users;
        }

        public static List<String> GetStatusNames(this Ticket ticket)
        {
            List<String> statuses = new List<String>();
            using (var db = new TicketContext())
            {
                foreach (TicketStatus t in db.TicketStatuses)
                {
                    statuses.Add(t.StatusName);
                }
            }
            return statuses;
        }

        public static Ticket UpdateTicketStatus(this Ticket ticket, TicketStatus status, User UpdatedBy=null) 
        {
            using (var db = new TicketContext())
            {
                var t = db.Tickets.FirstOrDefault(c => c.ID == ticket.ID);
                t.Status = status;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdatedBy = UpdatedBy;
                var tevent = new TicketEvent()
                {
                    EventName = "Ticket Status Updated",
                    Ticket = ticket,
                    Time = DateTime.Now
                };
                db.TicketEvents.Add(tevent);
                db.SaveChanges();
            }
            return ticket;
        }

        public static Ticket ProductReceived(this Ticket ticket)
        {
            using (var db = new TicketContext())
            {
                var ts = db.TicketStatuses.FirstOrDefault(c => c.StatusName == "Product Received");
                var tevent = new TicketEvent()
                {
                    EventName = "Product Received",
                    Ticket = ticket,
                    Time = DateTime.Now
                };
                db.TicketEvents.Add(tevent);
                db.SaveChanges();
                return ticket.UpdateTicketStatus(ts);
            }
        }

        public static Ticket CreateTicket(this Ticket ticket)
        {
            TicketStatus ts;
            using (var db = new TicketContext())
            {
                ts = db.TicketStatuses.FirstOrDefault(c => c.StatusName == "New Ticket");
                var tevent = new TicketEvent()
                {
                    EventName = "Ticket Created",
                    Ticket = ticket,
                    Time = DateTime.Now
                };
                ticket.Status = ts;
                db.TicketEvents.Add(tevent);
                db.SaveChanges();
                return ticket;
            }
        }


    }
}