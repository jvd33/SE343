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
            var userStore = new UserStore<User>(context);
            var manager = new UserManager<User>(userStore);

            var user = new User()
            {
                UserName = "admin",
                Email = "jvd5839@rit.edu",
            };

            IdentityResult result = manager.Create(user, "tempPass1234567");

            var statuses = new List<String>()
            {
                "Active - Product Received",
                "Active - New",
                "Closed - Refurbish Successful",
                "Active - Refurbish in progress",
                "Active - Updated",
                "Closed"
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