using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KennUTicket.Models;
using KennUTicket.Extensions;

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
            var SysUser = new User() { Username = "Admin", HashedPassword = "not yet implemented", Email = "jvd5839@rit.edu" };
            var statuses = new List<String>()
            {
                "Product Received",
                "New Ticket",
            };

            statuses.ForEach(x =>
            {
                var y = new TicketStatus()
                {
                    StatusName = x
                };
            });


            var TestTicket = new Ticket()
            {
                AssignedTo = SysUser,
                Category = (int)TicketContext.TicketCategory.Hardware,
                CreatedBy = SysUser,
                CreatedDate = DateTime.Now,
                Description = "This product is broken",
                LastUpdateDate = DateTime.Now,
                LastUpdatedBy = SysUser,
                Priority = (int)TicketContext.TicketPriority.High,
                CustomerAddress = "RIT",
                OrderNumber = "Test Order Number",
                Title = "Test Ticket NUMBER ONE!",
                WearableNumber = "Test Wearable Number"
            }.CreateTicket();

        }
    }
}