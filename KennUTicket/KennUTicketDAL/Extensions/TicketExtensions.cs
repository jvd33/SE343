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
        private static List<String> ticketPriorities = new List<String>() { "Low", "Medium", "High" };
        public static List<string> TicketPriorities { get => ticketPriorities; set => ticketPriorities = value; }

        private static List<String> ticketCategories = new List<String>() { "Hardware", "Software", "Complaint", "Other" };
        
        public static List<String> GetCategories(this Ticket ticket)
        {
            return ticketCategories;
        }

        public static List<String> GetPriorities(this Ticket ticket)
        {
            return ticketPriorities;
        }

        public static List<User> GetUsers(this Ticket ticket)
        {
            List<String> users = new List<String>();
            using (var db = new TicketContext())
            {
                return db.Users.ToList();
            }
        }

        public static List<TicketStatus> GetStatusNames(this Ticket ticket)
        {
            using (var db = new TicketContext())
            {
                return db.TicketStatuses.ToList();
            }
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
                db.SaveChanges();
                return ticket.UpdateTicketStatus(ts);
            }
        }

        public static Ticket CreateTicket(this Ticket ticket, string username)
        {
            TicketStatus ts;
            using (var db = new TicketContext())
            {
                var u = db.Users.FirstOrDefault(c => c.UserName == username);
                ts = db.TicketStatuses.FirstOrDefault(c => c.StatusName == "New Ticket");
                Ticket nTicket = new Ticket()
                {
                    Category = ticket.Category,
                    Priority = ticket.Priority,
                    CreatedDate = DateTime.Now,
                    CustomerAddress = ticket.CustomerAddress,
                    WearableNumber = ticket.WearableNumber,
                    Title = ticket.Title,
                    OrderNumber = ticket.OrderNumber,
                    Description = ticket.Description,
                    AssignedToID = ticket.AssignedToID,
                    CreatedByID = u.Id,
                    LastUpdatedByID = u.Id,
                    LastUpdateDate = DateTime.Now,
                    StatusID = ts.ID
                };
                db.Tickets.Add(nTicket);
                db.SaveChanges();
                var tevent = new TicketEvent()
                {
                    EventName = "Ticket Created",
                    TicketID = nTicket.ID,
                    Time = DateTime.Now
                };
                db.TicketEvents.Add(tevent);
                db.SaveChanges();
                return nTicket;
            }
        }


    }
}