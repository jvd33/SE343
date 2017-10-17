using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KennUTicket.Models;
using KennUTicket.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KennUTicket.DAL
{
    public class TicketInitializer : DropCreateDatabaseIfModelChanges<TicketContext>
    {
        protected override void Seed(TicketContext context)
        {
            if(context.Users.FirstOrDefault() != null)
            {
                return;
            }

            var statuses = new List<String>()
            {
                "Product Received",
                "New Ticket",
                "Refurbish successful",
                "Refurbish in progress",
                ""
            };

            statuses.ForEach(x =>
            {
                var y = new TicketStatus()
                {
                    StatusName = x
                };
                context.TicketStatuses.Add(y);
            });

            context.SaveChanges();
        }
    }
}