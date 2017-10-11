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


    }
}