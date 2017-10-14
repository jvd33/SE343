using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KennUTicket.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KennUTicket.DAL
{
    public class TicketContext : IdentityDbContext<User>
    {
        public TicketContext() : base("KennUTicket_Data")
        {
            Database.SetInitializer(new TicketInitializer());
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketEvent> TicketEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}